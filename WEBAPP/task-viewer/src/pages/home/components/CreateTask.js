import React, { useState } from 'react';
import config  from '../config/config';
import { toast } from 'react-toastify';

const CreateTask = ({ onTaskCreated, onClose }) => {
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');
    
    const handleSubmit = async (event) => {
        try {
            event.preventDefault();
            toast.info(`Solicitando criação de uma nova tarefa para o servidor.`)
            const response = await fetch(`${config.API_URL}/api/tarefa`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ "title":title, "description":description}),
            });
        
            if (response.ok) {
                const createdTask = await response.json();
                onTaskCreated(createdTask);
                toast.success(`Solicitação de criação da tarefa efetuada com sucesso.`)
            } else {
                toast.error('Erro na criação da tarefa.');
                const errorData = await response.json();
                toast.error('Dados de erro:', errorData);
            }
        } catch (error) {
            toast.error('Erro ao efetuar a solicitação da tarefa:', error);
        };
    };

    const createCardStyle = {
        backgroundColor: '#f4f4f4',
        borderRadius: '10px',
        padding: '10px',
        marginBottom: '10px',
        boxShadow: '0px 0px 5px #c5c5c5',
        width: '600px',
    };

    const titleStyle = {
        display: 'flex',
        justifyContent: 'center'
    };

    const footer = {
        display: 'flex',
        marginTop: '20px',
        justifyContent: 'center'
    };

    const buttonStyle = {
        backgroundColor: '#0078d7',
        color: '#fff',
        width: '250px',
        border: 'none',
        borderRadius: '4px',
        padding: '8px',
        cursor: 'pointer',
        marginRight: '50px'
    };

    const buttonCloseStyle = {
        backgroundColor: '#ff0000',
        color: '#fff',
        border: 'none',
        borderRadius: '4px',
        padding: '8px',
        cursor: 'pointer',
    };
    
    const inputStyle = {
        padding: '5px',
        marginBottom: '10px',
        width: '460px',
        border: '1px solid #ccc',
        borderRadius: '4px'
    };

    const descriptionStyle = {
        fontWeight: 'bold',
        marginRight: '10px',
        display: 'flex',
        flexDirection: 'row',
        flexWrap: 'no-wrap',
        width: '600px',
    };

    const inputTextAreaStyle = {
        padding: '10px',
        marginBottom: '-10px',
        marginLeft: '34px',
        width: '450px',
        height: '100px',
        border: '1px solid #ccc',
        borderRadius: '4px'
    };

    const labelStyle = {
        fontWeight: 'bold',
        marginRight: '10px',
        display: 'inline-block',
        minWidth: '100px'
    };
    
    return (
        <form style={createCardStyle} onSubmit={handleSubmit}>
        <h2 style={titleStyle}>Criar uma nova tarefa</h2>
        <div>
            <label style={labelStyle} htmlFor="title">Título:</label>
            <input
            type="text"
            id="title"
            style={inputStyle}
            value={title}
            required
            onChange={(event) => setTitle(event.target.value)}
            />
        </div>
        <div style={descriptionStyle}>
            <label htmlFor="description">Descrição:</label>
            <textarea
            id="description"
            style={inputTextAreaStyle}
            value={description}
            required
            onChange={(event) => setDescription(event.target.value)}
            />
        </div>
        <div style={footer}>
            <button style={buttonStyle} type="submit">Criar Tarefa</button>
            <button style={buttonCloseStyle} type="button" onClick={onClose}>Cancelar</button>
        </div>
        </form>
    );
};

export default CreateTask;

  