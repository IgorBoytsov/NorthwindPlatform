import { useState, useCallback } from 'react';

export interface LoginFormValues {
  username: string;
  password: string;
}

export const useLoginForm = () => {
  const [values, setValues] = useState<LoginFormValues>({
    username: '',
    password: '',
  });
  const [errors, setErrors] = useState<Record<string, string>>({});
  const [isLoading, setIsLoading] = useState(false);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  const minUsernameLength = 3;
  const minPasswordLength = 8;

  const validate = useCallback(() => {
    const newErrors: Record<string, string> = {};

    if (!values.username.trim()) {
      newErrors.username = 'Логин обязателен';
    } else if (values.username.length < minUsernameLength) {
      newErrors.username = `Логин должен быть не менее ${minUsernameLength} символов`;
    }

    if (!values.password) {
      newErrors.password = 'Пароль обязателен';
    } else if (values.password.length < minPasswordLength) {
      newErrors.password = `Минимум ${minPasswordLength} символов`;
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  }, [values, minUsernameLength, minPasswordLength]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setValues(prev => ({ ...prev, [name]: value }));

    if (errors[name]) {
      setErrors(prev => {
        const copy = { ...prev };
        delete copy[name];
        return copy;
      });
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!validate()) return;

    setIsLoading(true);
    setErrorMessage(null);

    try {
      await new Promise(resolve => setTimeout(resolve, 1200));
      console.log('Успешная аутентификация');
    } catch (err) {
      setErrorMessage('Неверный логин или пароль');
      console.error('Ошибка входа', err);
    } finally {
      setIsLoading(false);
    }
  };

  return {
    values,
    errors,
    isLoading,
    errorMessage,
    minUsernameLength,
    handleChange,
    handleSubmit,
  };
};