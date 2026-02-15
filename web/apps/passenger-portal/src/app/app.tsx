import '../styles.scss';
import { Routes, Route, Navigate } from 'react-router-dom';
import { LoginPage } from '../pages/login/LoginPage';

export const App = () => {
  return (
    <Routes>
      <Route path="/" element={<Navigate to="/login" replace/>} />
      <Route path="/login" element={<LoginPage />} />
      <Route path="*" element={<div>404 - Страницы не найдена</div>} />
    </Routes>
  );
};

export default App;