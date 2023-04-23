import React, { useState, useEffect } from 'react';
import { ToastContainer, toast } from 'react-toastify';
import TaskCard from './components/TaskCard';
import ExpandedTaskCard from './components/ExpandedTaskCard';
import CreateTask from './components/CreateTask';
import config  from './config/config';

const HomePage = () => {
  const [tasks, setTasks] = useState([]);
  const [selectedTask, setSelectedTask] = useState(null);
  const [newTask, setNewTask] = useState(false);

  useEffect(() => {
    fetchTasks();
  }, []);

  const fetchTasks = async () => {
    try {
      const response = await fetch(`${config.API_URL}/api/tarefa/all`);
      if (response.ok) {
        const tasks = await response.json();
        setTasks(tasks.tarefas);
        console.log('Solicitação de busca de tarefas efetuada com sucesso.');
      } else {
        const errorData = await response.json();
        toast.error('Erro: ' + errorData);
      }
    } catch (error) {
      toast.error('Erro ao efetuar a solicitação de busca de tarefas:', error);
    }
  };

  const handleNewTask = () => {
    setNewTask(!newTask);
  };

  const handleTaskCreated = (createdTask) => {
    setTasks(tasks.map(task => task.id === createdTask.id ? createdTask : task));
    handleNewTask(false);
    setSelectedTask(createdTask);
    fetchTasks();
  };

  const handleCardClick = (id) => {
    const task = tasks.find((task) => task.id === id);
    setSelectedTask(task);
    console.log(`Solicitado detalhes da tarefa de id {${id}}`)
  };

  const handleCloseExpandedTaskCard = () => {
    setSelectedTask(null);
    setNewTask(false);
  };

  const handleTaskUpdated = (updatedTask) => {
    setTasks(tasks.map(task => task.id === updatedTask.id ? updatedTask : task));
    setSelectedTask(updatedTask);
    fetchTasks();
  };

  const handleTaskDeleted = (taskId) => {
    setTasks(tasks.filter(task => task.id !== taskId));
    setSelectedTask(null);
    fetchTasks();
  };

  const cardStyle = {
    display: 'flex', 
    flexDirection: 'row',
    flexWrap: 'wrap',
    justifyContent: 'center'
  };

  const homeStyle = {
    display: 'flex',
    flexDirection: 'column',
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  };
  
  const headerStyle = {
    display: 'flex',
    width: '100%',
    backgroundColor: 'darkcyan',
    color: 'white',
    justifyContent: 'center',
    alignItems: 'center',
    padding: '10px',
  };

  const titleStyle = {
    display: 'flex',
    alignSelf: 'center',
    flexGrow: 1,
    justifyContent: 'center'
  };
  
  const buttonStyle = {
    backgroundColor: '#00000000',
    fontSize: '60px',
    color: '#fff',
    border: 'none',
    borderRadius: '4px',
    padding: '8px',
    cursor: 'pointer',
    display: 'flex',
    alignSelf: 'center',
    marginLeft: 'auto'
  };
  
  return (
    <div style={homeStyle}>
      <div style={headerStyle}>
        <h1 style={titleStyle}>Tarefas:</h1>
        <button style={buttonStyle} onClick={handleNewTask}>+</button>
      </div>
        <div style={cardStyle}>
            {selectedTask && (
                <div>
                <ExpandedTaskCard
                    task={selectedTask}
                    onTaskUpdated={handleTaskUpdated}
                    onTaskDeleted={handleTaskDeleted}
                    onClose={handleCloseExpandedTaskCard}
                />
                <ToastContainer />
                </div>
            ) || newTask && (
                <div>
                <CreateTask 
                    onTaskCreated={handleTaskCreated} 
                    onClose={handleCloseExpandedTaskCard}
                />
                <ToastContainer />
                </div>
            ) || tasks.map(task => (
            <div>
            <TaskCard
                key={task.id}
                task={task}
                onCardClick={() => handleCardClick(task.id)}
            />
            <ToastContainer />
            </div>
            ))}
        </div>
    </div>
  );
};

export default HomePage;
