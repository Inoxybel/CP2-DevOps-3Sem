const cardStyle = {
  display: 'flex',
  flexDirection: 'column',
  backgroundColor: '#f4f4f4',
  borderRadius: '10px',
  padding: '10px',
  marginBottom: '10px',
  marginLeft: '10px',
  cursor: 'pointer',
  boxShadow: '0px 0px 5px #c5c5c5',
  width: '350px',
  height: '200px'
};

const pStyle = {
  marginTop: '0px'
};

const headerStyle = {
  display: 'flex',
  alignItems: 'left',
  borderBottom: '3px solid #eaeaea',
  marginBottom: '16px',
};

const idStyle = {
  margin: 0,
  color: 'blue'
};

const titleStyle = {
  marginLeft: 5,
  marginTop: 2
};

const labelFooter = {
  display: 'flex',
  alignItems: 'flex-end',
  justifyContent: 'flex-end',
  color: '#00000088'
};

const linkStyle = {
  textDecoration: "none",
  color: "inherit"
}

const formatDate = (date) => {
  if (!date) return 'N/A';
  const options = { year: 'numeric', month: '2-digit', day: '2-digit' };
  return new Date(date).toLocaleDateString('pt-BR', options);
}

const getDateString = (task) => {
  switch (task.state) {
    case 0:
      return <p style={pStyle}><strong>Data de Criação: </strong> {formatDate(task.createdDate)}</p>;
    case 1:
      return <p style={pStyle}><strong>Data de Ativação: </strong> {formatDate(task.activedDate)}</p>;
    case 2:
      return <p style={pStyle}><strong>Data de Resolução: </strong> {formatDate(task.resolvedDate)}</p>;
    case 3:
      return <p style={pStyle}><strong>Data de Encerramento: </strong> {formatDate(task.closedDate)}</p>;
    default:
      return null;
  }
}

const getStatusString = (state) => {
  switch (state) {
    case 0:
      return <a href="#" style={linkStyle}>Novo</a>;
    case 1:
      return <a href="#" style={linkStyle}>Ativo</a>;
    case 2:
      return <a href="#" style={linkStyle}>Resolvido</a>;
    case 3:
      return <a href="#" style={linkStyle}>Finalizado</a>;
    default:
      return <a href="#" style={linkStyle}>Status Inválido</a>;
  }
}

const TaskCard = ({ task, onCardClick }) => {
  return (
    <div style={cardStyle} onClick={() => onCardClick()}>
      <div style={headerStyle}>
        <h3 style={idStyle}>{task.id.slice(0, 4)} - </h3><a style={titleStyle}>{task.title}</a>
      </div>
      <p style={pStyle}><strong>Responsável:</strong> {task.assignedTo || 'N/A'}</p>
      <p style={pStyle}><strong>Status:</strong> {getStatusString(task.state)}</p>
      {getDateString(task)}
      <p style={labelFooter}>Clique no card para ver mais detalhes</p>
    </div>
  );
};

export default TaskCard;
