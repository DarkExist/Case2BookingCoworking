import React, { useState } from 'react';
import { Table, Button, Modal, Form, Input, message } from 'antd';
import { getUserData, getFData, getReservations } from '../PanelMain'; // получаем данные пользователей и резервирований

// Типы данных для резервирования
interface Reservation {
  id: number;
  coworking: string;
  timeSlots: { startTime: string; endTime: string; bookedBy: string }[];
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
  const [selectedReservation, setSelectedReservation] = useState<Reservation | null>(null);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [editingSlot, setEditingSlot] = useState<{ index: number; startTime: string; endTime: string } | null>(null);

  const handleCancelBooking = (coworkingId: number, index: number) => {
    const updatedReservations = reservations.map((reservation) => {
      if (reservation.id === coworkingId) {
        const updatedTimeSlots = reservation.timeSlots.filter((_, i) => i !== index); // Удаляем интервал
        return { ...reservation, timeSlots: updatedTimeSlots };
      }
      return reservation;
    });

    setReservations(updatedReservations);
    message.success('Бронирование отменено');
  };

  const handleEditBooking = (coworkingId: number, index: number, newStartTime: string, newEndTime: string) => {
    const updatedReservations = reservations.map((reservation) => {
      if (reservation.id === coworkingId) {
        const updatedTimeSlots = reservation.timeSlots.map((slot, i) =>
          i === index ? { ...slot, startTime: newStartTime, endTime: newEndTime } : slot
        );
        return { ...reservation, timeSlots: updatedTimeSlots };
      }
      return reservation;
    });

    const lol = getReservations();
    console.log(lol)

    setReservations(updatedReservations);
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
      render: (timeSlots: { startTime: string; endTime: string; bookedBy: string }[], _, record: Reservation) => (
        timeSlots.map((slot, index) => (
          <div key={index}>
            {slot.startTime} - {slot.endTime} (Забронировано: {slot.bookedBy})
            <Button type="link" danger onClick={() => handleCancelBooking(record.id, index)}>
              Отменить
            </Button>
            <Button type="link" onClick={() => { setEditingSlot({ index, startTime: slot.startTime, endTime: slot.endTime }); setIsModalVisible(true); }}>
              Изменить
            </Button>
          </div>
        ))
      ),
    },
    {
      title: 'Действия',
      key: 'action',
      render: (_, record: Reservation) => (
        <Button
          type="primary"
          onClick={() => handleEditBooking(record.id, 0, '10:00', '12:00')} // Пример с изменением времени
        >
          Изменить бронирование
        </Button>
      ),
    },
  ];

  // Функция для сохранения изменений времени
  const handleSaveEdit = (values: any) => {
    if (editingSlot && selectedReservation) {
      const { startTime, endTime } = values;
      handleEditBooking(selectedReservation.id, editingSlot.index, startTime, endTime);
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
          onFinish={handleSaveEdit} // Когда форма отправляется, вызывается handleSaveEdit
        >
          <Form.Item
            label="Начало"
            name="startTime"
            initialValue={editingSlot?.startTime}
            rules={[{ required: true, message: 'Введите время начала!' }]}
          >
            <Input type="time" />
          </Form.Item>
          <Form.Item
            label="Конец"
            name="endTime"
            initialValue={editingSlot?.endTime}
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