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