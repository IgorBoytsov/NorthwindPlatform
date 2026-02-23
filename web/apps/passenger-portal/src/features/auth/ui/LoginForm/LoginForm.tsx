import { useLoginForm } from '../../model';
import styles from './LoginForm.module.scss';

export const LoginForm = () => {
    const {
        values,
        errors,
        isLoading,
        errorMessage,
        handleChange,
        handleSubmit,
    } = useLoginForm();

    return (
        <div className={styles['login-page']}>
            <div className={styles['form-container']}>
                <p className={styles.title}>Авторизация</p>
                
                <form onSubmit={handleSubmit} className={styles['login-form']}>
                    <div className={styles['form-field']}>
                        <label htmlFor="username">Логин</label>
                        <input  
                            id="username" 
                            name="username" 
                            type="text" 
                            value={values.username} onChange={handleChange} 
                            className={errors.username ? `${styles['form-field']} ${styles.error}` : ''}
                            />   

                        {errors.username && (<small className={styles['error-message']}>{errors.username}</small>)}
                    </div>

                     <div className={styles['form-field']}>
                        <label htmlFor="password">Пароль</label>
                        <input
                        id="password"
                        name="password"
                        type="password"
                        value={values.password}
                        onChange={handleChange}
                        className={ errors.password ? `${styles['form-field']} ${styles.error}` : ''}
                        />

                        {errors.password && (<small className={styles['error-message']}>{errors.password}</small>)}
                    </div>
                    
                    {errorMessage && (<small className={`${styles.alert} ${styles['alert-error']}`}> {errorMessage} </small>)}
                    
                    <button
                        type="submit"
                        disabled={isLoading || Object.keys(errors).length > 0}
                        className={styles['btn-submit']}>  
                        {isLoading ? 'Вход...' : 'Войти'}
                    </button>

                </form>
            </div>
        </div>
    );
}
