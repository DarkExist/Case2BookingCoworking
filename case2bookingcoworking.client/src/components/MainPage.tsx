import React, { useState, useEffect } from 'react';
import { Table, Button, TimePicker, Modal, Form, message, Tooltip } from 'antd';
import type { ColumnsType } from 'antd/es/table';
import { getUserData } from '../PanelMain';

const { RangePicker } = TimePicker;

interface Reservation {
    id: number;
    coworking: string;
    timeSlots: { startTime: string; endTime: string; bookedBy: string }[];
}

// Чтение данных из localStorage при первой загрузке
const loadInitialData = (): Reservation[] => {
    const storedData = localStorage.getItem('Reservations');
    if (storedData) {
        return JSON.parse(storedData);
    }
    return [
        { id: 1, coworking: 'Коворкинг 1', timeSlots: [] },
        { id: 2, coworking: 'Коворкинг 2', timeSlots: [] },
        { id: 3, coworking: 'Коворкинг 3', timeSlots: [] },
        { id: 4, coworking: 'Коворкинг 4', timeSlots: [] },
        { id: 5, coworking: 'Коворкинг 5', timeSlots: [] },
        { id: 6, coworking: 'Коворкинг 6', timeSlots: [] },
    ];
};

const MainPage: React.FC = () => {
    const [data, setData] = useState<Reservation[]>(loadInitialData());
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [selectedCoworking, setSelectedCoworking] = useState<number | null>(null);

    const userData = getUserData(); // Получаем данные о пользователе

    // Сохраняем данные в localStorage при каждом обновлении
    useEffect(() => {
        localStorage.setItem('Reservations', JSON.stringify(data));
    }, [data]);

    const checkOverlap = (
        existingSlots: { startTime: string; endTime: string }[],
        newSlot: { startTime: string; endTime: string }
    ) => {
        return existingSlots.some(
            (slot) =>
                newSlot.startTime < slot.endTime && newSlot.endTime > slot.startTime
        );
    };

    const handleAddTimeSlot = (values: any) => {
        if (selectedCoworking !== null) {
            const newSlot = {
                startTime: values.time[0].format('HH:mm'),
                endTime: values.time[1].format('HH:mm'),
                bookedBy: userData.name || 'Неизвестный пользователь',
            };

            const updatedData = data.map((item) => {
                if (item.id === selectedCoworking) {
                    if (checkOverlap(item.timeSlots, newSlot)) {
                        message.error('Временной интервал пересекается с существующим!');
                        return item;
                    }

                    return {
                        ...item,
                        timeSlots: [...item.timeSlots, newSlot],
                    };
                }
                return item;
            });

            setData(updatedData);
        }
        setIsModalVisible(false);
    };

    const handleDeleteTimeSlot = (coworkingId: number, index: number) => {
        const updatedData = data.map((item) => {
            if (item.id === coworkingId) {
                const updatedTimeSlots = item.timeSlots.filter((_, i) => i !== index); // Удаляем по индексу
                return {
                    ...item,
                    timeSlots: updatedTimeSlots,
                };
            }
            return item;
        });

        // Проверяем, произошло ли изменение
        const intervalRemoved = updatedData.some(
            (item, idx) => item.timeSlots.length !== data[idx].timeSlots.length
        );

        if (intervalRemoved) {
            setData(updatedData); // Обновляем состояние
            message.success('Временной интервал успешно удалён!');
        } else {
            message.error('Не удалось удалить интервал. Попробуйте снова!');
        }
    };

    const columns: ColumnsType<Reservation> = [
        {
            title: 'Коворкинг',
            dataIndex: 'coworking',
            key: 'coworking',
        },
        {
            title: 'Забронированные интервалы',
            dataIndex: 'timeSlots',
            key: 'timeSlots',
            render: (timeSlots: { startTime: string; endTime: string; bookedBy: string }[], record) =>
                timeSlots.map((slot, index) => (
                    <Tooltip
                        key={index}
                        title={
                            <div>
                                <p><strong>Имя:</strong> {slot.bookedBy}</p>
                                {userData.phone && <p><strong>Телефон:</strong> {userData.phone}</p>}
                                {userData.telegramTag && <p><strong>Telegram:</strong> {userData.telegramTag}</p>}
                            </div>
                        }
                    >
                        <div
                            style={{
                                display: 'flex',
                                justifyContent: 'space-between',
                                marginBottom: 4,
                                backgroundColor: '#f5f5f5',
                                padding: '4px',
                                borderRadius: '4px',
                                cursor: 'pointer',
                            }}
                        >
                            <span>
                                {slot.startTime} - {slot.endTime}
                            </span>


                            {(JSON.parse(localStorage.getItem("user")!).name === slot.bookedBy
                                || JSON.parse(localStorage.getItem("user")!).name === "admin")
                                && <Button
                                    type="link"
                                    danger
                                    onClick={() => handleDeleteTimeSlot(record.id, index)}
                                >
                                    Удалить
                                </Button>}

                        </div>
                    </Tooltip>
                )),
        },
        {
            title: 'Действия',
            key: 'action',
            render: (_, record) => (
                <Button
                    type="primary"
                    onClick={() => {
                        setSelectedCoworking(record.id);
                        setIsModalVisible(true);
                    }}
                >
                    Добавить интервал
                </Button>
            ),
        },
    ];

    return (
        <div>
            <h2>Календарь бронирования</h2>
            <Table columns={columns} dataSource={data} rowKey="id" pagination={false} />
            <Modal
                title="Добавить интервал"
                open={isModalVisible}
                onCancel={() => setIsModalVisible(false)}
                footer={null}
            >
                <Form onFinish={handleAddTimeSlot}>
                    <Form.Item
                        name="time"
                        label="Выберите время"
                        rules={[{ required: true, message: 'Пожалуйста, выберите время!' }]}
                    >
                        <RangePicker format="HH:mm" minuteStep={5} />
                    </Form.Item>
                    <Form.Item>
                        <Button type="primary" htmlType="submit" block>
                            Сохранить
                        </Button>
                    </Form.Item>
                </Form>
            </Modal>
        </div>
    );
};

export default MainPage;