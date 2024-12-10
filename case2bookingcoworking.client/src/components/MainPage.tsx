import React, { useState } from 'react';
import { Table, Button, TimePicker, Modal, Form, message } from 'antd';
import type { ColumnsType } from 'antd/es/table';

const { RangePicker } = TimePicker;

interface Reservation {
  id: number;
  coworking: string;
  timeSlots: { startTime: string; endTime: string }[];
}

const initialData: Reservation[] = [
  { id: 1, coworking: 'Коворкинг 1', timeSlots: [] },
  { id: 2, coworking: 'Коворкинг 2', timeSlots: [] },
  { id: 3, coworking: 'Коворкинг 3', timeSlots: [] },
  { id: 4, coworking: 'Коворкинг 4', timeSlots: [] },
  { id: 5, coworking: 'Коворкинг 5', timeSlots: [] },
  { id: 6, coworking: 'Коворкинг 6', timeSlots: [] },
];

const MainPage: React.FC = () => {
  const [data, setData] = useState(initialData);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [selectedCoworking, setSelectedCoworking] = useState<number | null>(null);

  // Проверка пересечения временных интервалов
  const checkOverlap = (
    existingSlots: { startTime: string; endTime: string }[],
    newSlot: { startTime: string; endTime: string }
  ) => {
    return existingSlots.some(
      (slot) =>
        (newSlot.startTime < slot.endTime && newSlot.endTime > slot.startTime) // Пересечение интервалов
    );
  };

  const handleAddTimeSlot = (values: any) => {
    if (selectedCoworking !== null) {
      const newSlot = {
        startTime: values.time[0].format('HH:mm'),
        endTime: values.time[1].format('HH:mm'),
      };

      const updatedData = data.map((item) => {
        if (item.id === selectedCoworking) {
          // Проверка на пересечение интервалов
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
        return {
          ...item,
          timeSlots: item.timeSlots.filter((_, i) => i !== index),
        };
      }
      return item;
    });
    setData(updatedData);
    message.success('Временной интервал успешно удалён!');
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
      render: (timeSlots: { startTime: string; endTime: string }[], _, record) =>
        timeSlots.map((slot, index) => (
          <div key={index} style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 4 }}>
            <span>
              {slot.startTime} - {slot.endTime}
            </span>
            <Button
              type="link"
              danger
              onClick={() => handleDeleteTimeSlot(record.id, index)}
            >
              Удалить
            </Button>
          </div>
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
        visible={isModalVisible}
        onCancel={() => setIsModalVisible(false)}
        footer={null}
      >
        <Form onFinish={handleAddTimeSlot}>
          <Form.Item
            name="time"
            label="Выберите время"
            rules={[{ required: true, message: 'Пожалуйста, выберите время!' }]}
          >
            <RangePicker format="HH:mm" />
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