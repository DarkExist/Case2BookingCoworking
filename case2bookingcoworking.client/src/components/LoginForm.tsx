import React from 'react';
import { Form, Input, Button, Checkbox, message } from 'antd';
import { LockOutlined } from '@ant-design/icons';
import { saveUserData, getFData } from '../PanelMain';

interface LoginFormProps {
  onLogin: () => void;
  onSwitchToRegister: () => void;
}

const LoginForm: React.FC<LoginFormProps> = ({ onLogin, onSwitchToRegister }) => {
  const onFinish = (values: any) => {
    // прошел проверку или нет
    const DataF = getFData();
    if (DataF[values.email]) {
      if (DataF[values.email].password == values.password) {
        console.log('Logged in with:', values);
        saveUserData(DataF[values.email].name, values.email, DataF[values.email].password, DataF[values.email].phone, DataF[values.email].telegramTag);
        console.log(DataF[values.email].name, values.email, DataF[values.email].password, DataF[values.email].phone, DataF[values.email].telegramTag)
        onLogin();
      }
      else {
        console.log(values.password);
        console.log(DataF);
        message.error('Пароль не верный!');
      }
    }
    else {
      message.error('Аккаунт не найден!');
    }
  };

  return (
    <Form
      name="login_form"
      initialValues={{ remember: true }}
      onFinish={onFinish}
    >
      <Form.Item
        name="email"
        rules={[
          { required: true, message: 'Пожалуйста, введите ваш email!' },
          { type: 'email', message: 'Неверный формат email!' },
        ]}
      >
        <Input prefix={<LockOutlined />} />
      </Form.Item>

      <Form.Item
        name="password"
        rules={[{ required: true, message: 'Пожалуйста, введите ваш пароль!' }]}
      >
        <Input.Password prefix={<LockOutlined />} placeholder="Пароль" />
      </Form.Item>

      <Form.Item name="remember" valuePropName="checked">
        <Checkbox
        style={{
          color: 'white',
        }}
        >Запомнить меня</Checkbox>
      </Form.Item>

      <Form.Item>
        <Button type="primary" htmlType="submit" block>
          Войти
        </Button>
      </Form.Item>

      <Form.Item>
        <Button type="link" block onClick={onSwitchToRegister}>
          Нет аккаунта? Зарегистрируйтесь!
        </Button>
      </Form.Item>
    </Form>
  );
};

export default LoginForm;