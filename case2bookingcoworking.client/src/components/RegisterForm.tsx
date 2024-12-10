import React from 'react';
import { Form, Input, Button, Checkbox, message } from 'antd';
import { useNavigate } from 'react-router-dom';
import { saveUserData, saveFData } from '../PanelMain';

interface RegisterFormProps {
  onRegister: () => void;
}

const RegistrationForm: React.FC<RegisterFormProps> = ({ onRegister }) => {
    const [form] = Form.useForm();
    const navigate = useNavigate();

  const handleSubmit = async (values: any) => {
    message.success('Регистрация успешна!');
    saveUserData(values.username, values.email, values.password, '', '');
    saveFData(values.username, values.email, values.password, '', '');
      console.log(values);
      navigate("/login")
      
  };

  return (
    <div style={{ maxWidth: 400, margin: '0 auto' }}>
      <h2>Форма регистрации</h2>
      <Form
        form={form}
        onFinish={handleSubmit}
        layout="vertical"
        initialValues={{ remember: true }}
      >
        {/* Имя пользователя */}
        <Form.Item
          
          label={<span style={{ color: 'white' }}>Имя пользователя</span>}
          name="username"
          rules={[{ required: true, message: 'Пожалуйста, введите ваше имя!' }]}
        >
          <Input />
        </Form.Item>

        {/* Email */}
        <Form.Item
          label={<span style={{ color: 'white' }}>Email</span>}
          name="email"
          rules={[
            { required: true, message: 'Пожалуйста, введите ваш email!' },
            { type: 'email', message: 'Неверный формат email!' },
          ]}
        >
          <Input />
        </Form.Item>

        {/* Пароль */}
        <Form.Item
          label={<span style={{ color: 'white' }}>Пароль</span>}
          name="password"
          rules={[
            { required: true, message: 'Пожалуйста, введите ваш пароль!' },
            { min: 6, message: 'Пароль должен быть минимум 6 символов!' },
          ]}
          hasFeedback
        >
          <Input.Password />
        </Form.Item>

        {/* Подтверждение пароля */}
        <Form.Item
          label={<span style={{ color: 'white' }}>Подтверждение пароля</span>}
          name="confirmPassword"
          dependencies={['password']}
          rules={[
            { required: true, message: 'Пожалуйста, подтвердите ваш пароль!' },
            ({ getFieldValue }) => ({
              validator(_, value) {
                if (!value || getFieldValue('password') === value) {
                  return Promise.resolve();
                }
                return Promise.reject(new Error('Пароли не совпадают!'));
              },
            }),
          ]}
          hasFeedback
        >
          <Input.Password />
        </Form.Item>

        {/* Кнопка отправки формы */}
        <Form.Item>
          <Button type="primary" htmlType="submit" block>
            Зарегистрироваться
          </Button>
        </Form.Item>

        <Form.Item>
          <Button type="link" block onClick={onRegister}>
            Уже есть аккаунт? Войдите!
          </Button>
        </Form.Item>
      </Form>
    </div>
  );
};

export default RegistrationForm;