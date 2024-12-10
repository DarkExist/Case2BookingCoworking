import { message } from "antd";

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

export const saveUserData = (name: string, email: string, password: string, phone: string, telegramTag: string) => {
    const userData = { name, email, password, phone, telegramTag };
    localStorage.setItem('user', JSON.stringify(userData));
};

export const getUserData = () => {
    const userData = localStorage.getItem('user');
    console.log(userData);
    if (userData) {
        return JSON.parse(userData);
    }
    return null;
};

export const saveFData = (name: string, email: string, password: string, phone: string, telegramTag: string) => {
    // Получаем данные из localStorage
    const storedData = localStorage.getItem('DataF');
    
    // Если данных нет, создаем новый объект
    let DataF: { [key: string]: any } = storedData ? JSON.parse(storedData) : {};
  
    // Получаем текущие данные для пользователя, если они есть
    const existingData = DataF[email] || {};
  
    // Обновляем данные пользователя только для измененных полей
    DataF[email] = {
      name: name || existingData.name,
      password: password || existingData.password,
      phone: phone || existingData.phone,
      telegramTag: telegramTag || existingData.telegramTag,
    };
  
    // Сохраняем обновленные данные обратно в localStorage
    localStorage.setItem('DataF', JSON.stringify(DataF));
};

export const getFData = () => {
    const DataF = localStorage.getItem('DataF');
    console.log(DataF);
    if (DataF) {
        return JSON.parse(DataF);
    }
    return null;
};

export const getReservations = () => {
    const DataF = localStorage.getItem('Reservations');
    console.log(DataF);
    if (DataF) {
        return JSON.parse(DataF);
    }
    return null;
};

export const setAllReservations = (data: Reservation[]) => {
    localStorage.setItem('Reservations', JSON.stringify(data));
};

export const addReservation = (
    coworkingId: number,
    newSlot: TimeSlot,
    reservations: Reservation[],
    setReservations: (data: Reservation[]) => void
    ) => {
    // Проверка пересечений с существующими интервалами
    const checkOverlap = (existingSlots: TimeSlot[], newSlot: TimeSlot) => {
        return existingSlots.some(
        (slot) =>
            newSlot.startTime < slot.endTime && newSlot.endTime > slot.startTime
        );
    };

    const updatedReservations = reservations.map((reservation) => {
        if (reservation.id === coworkingId) {
        if (checkOverlap(reservation.timeSlots, newSlot)) {
            message.error('Данный временной интервал уже занят!');
            return reservation;
        }
        return {
            ...reservation,
            timeSlots: [...reservation.timeSlots, newSlot],
        };
        }
        return reservation;
    });

    // Обновляем данные в состоянии и localStorage
    setReservations(updatedReservations);
    localStorage.setItem('reservations', JSON.stringify(updatedReservations));
    message.success('Бронирование успешно добавлено!');
};
