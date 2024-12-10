import React from 'react';
import { Form, Input, Button, message } from 'antd';
import { getUserData, saveUserData, saveFData } from '../PanelMain';

const ProfilePage: React.FC = () => {
  let userData = getUserData()

  const handleSubmit = (values: any) => {
    message.success('Готово!');
    // обработать отправку данных на сервер
    saveUserData(values.name, userData.email, values.password, values.phone, values.telegramTag);
    saveFData(values.name, userData.email, values.password, values.phone, values.telegramTag)
    console.log(values);
  };

  return (
    <div style={{ maxWidth: '400px', margin: '50px auto', padding: '20px', border: '1px solid #ccc', borderRadius: '8px' }}>
      <h2 style={{ textAlign: 'center' }}>Личный кабинет</h2>

      <Form
        name="profile-form"
        onFinish={handleSubmit}
        initialValues={{
          name: userData.name,
          phone: userData.phone,
          telegramTag: userData.telegramTag,
        }}
        layout="vertical"
      >
        <Form.Item label="Email" name="email">
          <p style={{ color: 'gray', fontSize: '12px' }}>Мы не будем рассылать вам спам.</p>
        </Form.Item>

        <Form.Item label="Имя" name="name" rules={[{ required: true, message: 'Пожалуйста, введите ваше имя!' }]}>
          <Input value={userData.name} />
        </Form.Item>

        <Form.Item label="Телефон" name="phone">
          <Input value={userData.phone} />
        </Form.Item>

        <Form.Item label="Telegram Тег" name="telegramTag">
          <Input value={userData.telegramTag} />
        </Form.Item>

        <Form.Item>
          <Button type="primary" htmlType="submit" block>
            Обновить данные
          </Button>
        </Form.Item>
      </Form>
    </div>
  );
};

export default ProfilePage;
