import { useState, useCallback } from 'react';
import { SrpService } from '@quantropic/security'
import { SrpChallengeRequest, SrpVerifyRequest } from '@northwindplatform/shared/contracts'
import { useAuthApi } from '../api/auth.api';

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

  const srpService = new SrpService();
  const { getSrpChallenge, srpVerifyProof } = useAuthApi();

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

  const handleSubmit = async (e: React.SyntheticEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (!validate()) return;

    const newErrors: Record<string, string> = {};

    setIsLoading(true);
    setErrorMessage(null);

    try {
      const { username, password } = values;
      const srpChallengeRequest: SrpChallengeRequest = { login: username };
      const srpChallengeResponse = await getSrpChallenge(srpChallengeRequest);
      const { salt, b } = srpChallengeResponse;
      const { A, M1, S } = await srpService.generateSrpProof(username, password, salt, b);

      const srpVerifyRequest : SrpVerifyRequest = { Login: username, A, M1 };

      const srpVerifierResponse = await srpVerifyProof(srpVerifyRequest); 
      const { m2 } = srpVerifierResponse;

      if (!m2) {
        newErrors.m2 = "Ошибка аутентификации: M2 отсутствует в ответе сервера.";
        return;
      }

      const isServerValid = await srpService.verifyServerM2(A, M1, S, m2);

      if (!isServerValid) {
        newErrors.errorMessage = "Ошибка аутентификации: Подлинность сервера не подтверждена!";
        return;
      }

      console.log("Успешная аутентификация! Сервер подтвержден.");
      
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