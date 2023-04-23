import React, { useState } from 'react';
import { toast } from 'react-toastify';
import config from '../config/config';

const ExpandedTaskCard = ({ task, onTaskUpdated, onTaskDeleted, onClose }) => {
  const [isEditing, setIsEditing] = useState(false);
  const [editedTask, setEditedTask] = useState({ ...task });

  const taskStates = [
    { value: 0, label: 'Novo' },
    { value: 1, label: 'Ativo' },
    { value: 2, label: 'Resolvido' },
    { value: 3, label: 'Finalizado' }
  ];

  const handleEditClick = async () => {
    if (isEditing) {
      toast.info(`Solicitando edição da tarefa de id {${task.id}} para o servidor`)
      try {
        const response = await fetch(`${config.API_URL}/api/tarefa/${task.id}`, {
          method: 'PUT',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(editedTask)
        });
  
        if (response.ok) {
          const updatedTask = await response.json();
          onTaskUpdated(updatedTask);
          toast.success(`Atualização concluída com sucesso.`)
        } else {
          toast.error(`Erro no servidor ao atualizar a tarefa de id {${task.id}}`)
          const errorData = await response.json();
          toast.error('Dados de erro:', errorData);
        }
      } catch (error) {
        toast.error('Erro na aplicação web ao fazer a solicitação PUT:', error);
      }
    }
    setIsEditing(!isEditing);
  };  

  const handleCancelClick = () => {
    setEditedTask(task);
    setIsEditing(false);
  };

  const handleChange = (event, field) => {
    const value = field === 'state' ? parseInt(event.target.value) : event.target.value;
    setEditedTask({ ...editedTask, [field]: value });
  };  

  const handleDeleteClick = async () => {
    toast.info(`Solicitando a exclusão da tarefa de id {${task.id}, aguardando confirmação.}`)
    if (window.confirm('Deseja realmente excluir essa tarefa?')) {
      toast.info(`Solicitação de exclusão confirmada. Enviando ao servidor.`)
      const response = await fetch(`${config.API_URL}/api/tarefa/${task.id}`, {
        method: 'DELETE'
      });

      if (response.ok) {
        onTaskDeleted(task.id);
        toast.success(`Solicitação de exclusão da tarefa de id {${task.id}} efetuada com sucesso`)
      } else {
        toast.error('Erro na aplicação web ao solicitar exclusão da tarefa');
      }
    }
  };

  const expandedCardStyle = {
    backgroundColor: '#f4f4f4',
    borderRadius: '10px',
    padding: '10px',
    marginBottom: '10px',
    boxShadow: '0px 0px 5px #c5c5c5',
    width: '600px',
  };
    
  const labelStyle = {
    fontWeight: 'bold',
    marginRight: '10px',
    display: 'inline-block',
    minWidth: '100px'
  };
    
  const valueStyle = {
    display: 'inline-block',
    minWidth: '100px',
    marginBottom: '5px'
  };

  const dateValueStyle = {
    fontWeight: 'bold',
    marginRight: '10px',
    display: 'inline-block',
    width: '180px'
  };
    
  const buttonStyle = {
    backgroundColor: '#0078d7',
    color: '#fff',
    border: 'none',
    borderRadius: '4px',
    padding: '8px',
    cursor: 'pointer',
    marginRight: '10px',
  };

  const buttonCloseStyle = {
    backgroundColor: '#ff0000',
    color: '#fff',
    border: 'none',
    borderRadius: '4px',
    padding: '8px',
    cursor: 'pointer',
    marginLeft: '525px',
  };
    
  const inputStyle = {
    padding: '5px',
    marginBottom: '10px',
    width: '460px',
    border: '1px solid #ccc',
    borderRadius: '4px'
  };

  const descriptionStyle = {
    display: 'flex',
    alignItems: 'center',
  };

  const inputTextAreaStyle = {
    padding: '10px',
    marginBottom: '-10px',
    width: '450px',
    height: '100px',
    border: '1px solid #ccc',
    borderRadius: '4px'
  };

  const selectStyle = {
    padding: '5px',
    marginBottom: '10px',
    width: '470px',
    border: '1px solid #ccc',
    borderRadius: '4px'
  };

  const footerStyle = {
    display: 'flex',
    justifyContent: 'center'
  };

  const formatDate = (date) => {
    if (!date) return 'N/A';
    const options = { year: 'numeric', month: '2-digit', day: '2-digit' };
    return new Date(date).toLocaleDateString('pt-BR', options);
  }
  
  return (
    <div className="expanded-task-card" style={expandedCardStyle}>
      <button style={buttonCloseStyle} onClick={onClose}>Fechar</button>
      <h2><span style={labelStyle}>Título: </span><span style={valueStyle}>{isEditing ? <input style={inputStyle} type="text" value={editedTask.title} onChange={(event) => handleChange(event, 'title')} /> : task.title}</span></h2>
      <p><span style={labelStyle}>ID do objeto:</span><span style={valueStyle}>{task.id}</span></p>
      <p><span style={labelStyle}>Responsável:</span><span style={valueStyle}>{isEditing ? <input style={inputStyle} type="text" value={editedTask.assignedTo || ''} onChange={(event) => handleChange(event, 'assignedTo')} /> : (task.assignedTo || 'N/A')}</span></p>
      <p style={descriptionStyle}>
        <span style={labelStyle}>Descrição:</span>
        <span style={valueStyle}>
          {isEditing ? 
            <textarea style={inputTextAreaStyle} value={editedTask.description} onChange={(event) => handleChange(event, 'description')} /> 
            : 
            <textarea style={inputTextAreaStyle} defaultValue={task.description} /> 
          }
        </span>
      </p>
      <p>
        <span style={labelStyle}>Status:</span>
        {isEditing ? (
          <select style={selectStyle} value={editedTask.state} onChange={(event) => handleChange(event, 'state')}>
            {taskStates.map(state => (
              <option key={state.value} value={state.value}>{state.label}</option>
            ))}
          </select>
        ) : (
          <span style={valueStyle}>{taskStates.find(state => state.value === task.state)?.label || 'N/A'}</span>
        )}
      </p>
      <p>
        <span style={dateValueStyle}>Data de Criação: </span> <span style={valueStyle}>{formatDate(task.createdDate)}</span><br/>
        <span style={dateValueStyle}>Data de Ativação: </span> <span style={valueStyle}>{formatDate(task.activedDate)}</span><br/>
        <span style={dateValueStyle}>Data de Resolução: </span> <span style={valueStyle}>{formatDate(task.resolvedDate)}</span><br/>
        <span style={dateValueStyle}>Data de Encerramento: </span> <span style={valueStyle}>{formatDate(task.closedDate)}</span>
      </p>
      <p style={footerStyle}>
        <button style={buttonStyle} onClick={handleEditClick}>{isEditing ? 'Salvar' : 'Editar'}</button>
        <button style={buttonStyle} onClick={isEditing ? handleCancelClick : handleDeleteClick}>{isEditing ? 'Cancelar' : 'Excluir'}</button>
      </p>
    </div>
  );
};

export default ExpandedTaskCard;
