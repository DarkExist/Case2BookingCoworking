import React, { useState } from 'react';
import { Table, Button, Modal, Form, Input, message } from 'antd';
import { setAllReservations, getReservations } from '../PanelMain'; // получаем данные пользователей и резервирований

interface TimeSlot {
    startTime: string;
    endTime: string;
    bookedBy: string;
}

interface Reservation {
    id: number;
    coworking: string;
    timeSlots: TimeSlot[];
}

// Типы данных для пользователя
interface User {
    name: string;
    email: string;
    phone?: string;
    telegramTag?: string;
}

const AdminPanel: React.FC = () => {
    const [reservations, setReservations] = useState<Reservation[]>(getReservations());
    const [selectedResNum, setSelectedResNum] = useState<number | null>(null);
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [editingSlot, setEditingSlot] = useState<{ index: number; startTime: string; endTime: string } | null>(null);

    const handleCancelBooking = (coworkingId: any, index: number) => {

        const removeTimeSlot = (reservationId: number, index: number): Reservation[] => {
            const updatedReservations = reservations.map((reservation) => {
                if (reservation.id === reservationId + 1) {
                    const updatedTimeSlots = reservation.timeSlots.filter((_, i) => i !== index);
                    return { ...reservation, timeSlots: updatedTimeSlots };
                }
                return reservation;
            });

            return updatedReservations;
        };
        const updatedTimeSlotsgg = removeTimeSlot(coworkingId, index);

        setReservations(updatedTimeSlotsgg);
        setAllReservations(updatedTimeSlotsgg);
        localStorage.setItem('reservations', JSON.stringify(updatedTimeSlotsgg));

        if (JSON.stringify(updatedTimeSlotsgg) !== JSON.stringify(reservations)) {
            message.success('Бронирование успешно отменено');
        } else {
            message.error('Не удалось отменить бронирование. Попробуйте снова.');
        }
    };

    const handleEditBooking = (
        coworkingId: number,
        index: number,
        newStartTime: string,
        newEndTime: string
    ) => {
        console.log(newStartTime, newEndTime);
    
        const timeSlots = reservations[coworkingId].timeSlots;
    
        const isOverlapping = timeSlots.some((slot, i) => {
            if (i === index) return false;
            const slotStart = new Date(`1970-01-01T${slot.startTime}:00`);
            const slotEnd = new Date(`1970-01-01T${slot.endTime}:00`);
            const newStart = new Date(`1970-01-01T${newStartTime}:00`);
            const newEnd = new Date(`1970-01-01T${newEndTime}:00`);
    
            return (
                (newStart >= slotStart && newStart < slotEnd) || // Новый старт попадает в существующий слот
                (newEnd > slotStart && newEnd <= slotEnd) || // Новый конец попадает в существующий слот
                (newStart <= slotStart && newEnd >= slotEnd) // Новый слот полностью охватывает существующий
            );
        });
    
        if (isOverlapping) {
            message.error('Новое время пересекается с существующими бронированиями!');
            return;
        }
    
        reservations[coworkingId].timeSlots[index].startTime = newStartTime;
        reservations[coworkingId].timeSlots[index].endTime = newEndTime;
    
        setAllReservations(reservations);
    
        message.success('Бронирование обновлено');
        setIsModalVisible(false);
    };
    
    const columns = [
        {
            title: 'Коворкинг',
            dataIndex: 'coworking',
            key: 'coworking',
        },
        {
            title: 'Забронированные интервалы',
            dataIndex: 'timeSlots',
            key: 'timeSlots',
            render: (timeSlots: { startTime: string; endTime: string; bookedBy: string }[], _: any, record: number) => (
                timeSlots.map((slot, index) => (
                    <div key={index}>
                        {slot.startTime} - {slot.endTime} (Забронировано: {slot.bookedBy})
                        <Button type="link" danger onClick={() => handleCancelBooking(record, index)}>
                            Отменить
                        </Button>
                        <Button type="link" onClick={() => { setEditingSlot({ index, startTime: slot.startTime, endTime: slot.endTime }); setSelectedResNum(record); setIsModalVisible(true); }}>
                            Изменить
                        </Button>
                    </div>
                ))
            ),
        },
    ];

    // Функция для сохранения изменений времени
    const handleSaveEdit = (values: any) => {
        if ((editingSlot?.index !== undefined)  && (selectedResNum !== undefined)) {
            const { startTime, endTime } = values;
            handleEditBooking(selectedResNum, editingSlot.index, startTime, endTime);
        }
    };

    return (
        <div>
            <h2>Панель администратора</h2>
            <Table columns={columns} dataSource={reservations} rowKey="id" pagination={false} />

            {/* Модальное окно для изменения интервала */}
            <Modal
                title="Изменить бронирование"
                visible={isModalVisible}
                onCancel={() => setIsModalVisible(false)}
                footer={null}
            >
                <Form
                    onFinish={(values) => handleSaveEdit({ ...values, index: editingSlot?.index })} // Передаем index из editingSlot
                    initialValues={{
                        startTime: editingSlot?.startTime,
                        endTime: editingSlot?.endTime,
                    }}
                    >
                    <Form.Item
                        label="Начало"
                        name="startTime"
                        rules={[{ required: true, message: 'Введите время начала!' }]}
                    >
                        <Input type="time" />
                    </Form.Item>
                    <Form.Item
                        label="Конец"
                        name="endTime"
                        rules={[{ required: true, message: 'Введите время конца!' }]}
                    >
                        <Input type="time" />
                    </Form.Item>
                    <Form.Item>
                        <Button type="primary" htmlType="submit">
                            Сохранить
                        </Button>
                    </Form.Item>
                </Form>
            </Modal>
        </div>
    );
};

export default AdminPanel;