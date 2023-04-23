## Sobre o projeto

    O projeto tem como objetivo gerenciar tarefas em um banco de dados NoSQL (MongoDB), fornecendo CRUD para executar ações que incluem validações de dados e requisições para o SGBD para o gerenciamento dos dados.

## Endpoints

- Tarefas
	- [Criar](#criar-tarefa)
	- [Atualizar](#atualizar-tarefa)
	- [Recuperar uma tarefa](#recuperar-uma-tarefa)
	- [Recuperar todas tarefas](#recuperar-todas-tarefas)
	- [Deletar uma tarefa](#deletar-tarefa)

---
---
---

### Criar Tarefa
`POST` /api/tarefa/criar
| Campo | Tipo | Obrigatório | Descrição
|:-----:|:----:|:-----------:|:--------:
| title | String | Sim | Título da tarefa
| description | String | Sim | Descrição da tarefa

**Exemplo de corpo do request**

```json
{
	"title":"Criar relatório de atividade da aplicação",
	"description":"Dado que ... Quando ... Então ..."
}
```
	
**Exemplo de corpo do response**
| Campo | Tipo | Descrição
|:-----:|:----:|:--------:|
| id | string | Indentificador numérico da tarefa
| title | String | Título da tarefa
| description | String | Descrição da tarefa
| state | int | Estado da tarefa, podendo ser: New, Active, Resolved e Closed
| assignedTo | String | Email corporativo do colaborador responsável pela tarefa
| createdDate | String | Data de criação da tarefa

	
```json
{
	"id":"4b9226ea-8b9f-4b01-b58a-3887a5b25f61",
	"title":"Criar relatório de atividade da aplicação XPTO",
	"description":"Dado que ... Quando ... Então ...",
	"state":0,
	"createdDate":"2023-04-07T19:00:00",
	"assignedTo":"joao.silva@empresa.com.br",
}
```

**Códigos de Resposta**
| Código | Descrição |
|:------:|:---------:|
| 201 | Tarefa cadastrada com sucesso
| 400 | Erro na requisição

---
---
---

### Atualizar Tarefa
`PUT` /api/tarefa/{id}
| Campo | Tipo | Obrigatório | Descrição
|:-----:|:----:|:-----------:|:--------:
| title | String | Não | Título da tarefa
| description | String | Não | Descrição da tarefa
| state | int | Não | Estado da tarefa, podendo ser: New, Active, Resolved e Closed
| assignedTo | String | Não | Email corporativo do colaborador responsável pela tarefa

**Exemplo de corpo do request**
```json
{
	"title":"Criar relatório de atividade da aplicação XPTO",
	"description":"Dado que ... Quando ... Então ...",
	"state":2,
	"assignedTo":"joao.silva@empresa.com.br",
}
```

	**Exemplo de corpo do response**
| Campo | Tipo | Descrição
|:-----:|:----:|:--------:|
| id | String | Indentificador numérico da tarefa
| title | String | Título da tarefa
| description | String | Descrição da tarefa
| state | int | Estado da tarefa, podendo ser: New, Active, Resolved e Closed
| assignedTo | String | Email corporativo do colaborador responsável pela tarefa
| createdDate | String | Data de criação da tarefa
| activeDate | String | Data que a atividade foi iniciada
| resolvedDate | String | Data que a atividade foi resolvida e aguarda aprovação
| closedDate | String | Data que a atividade foi aprovada e fechada

```json
{
	"id":"4b9226ea-8b9f-4b01-b58a-3887a5b25f61",
	"title":"Criar relatório de atividade da aplicação XPTO",
	"description":"Dado que ... Quando ... Então ...",
	"state":3,
	"assignedTo":"joao.silva@empresa.com.br",
	"createdDate":"2023-04-07T19:00:00",
	"activeDate":"2023-04-07T19:05:00",
	"resolvedDate":"2023-04-08T11:02:30",
	"closedDate":null,
}
```

**Códigos de Resposta**
| Código | Descrição
|:------:|:---------:|
| 200 | Tarefa atualizada com sucesso
| 400 | Erro na requisição
| 404 | Tarefa não encontrada

---
---
---

### Recuperar uma Tarefa
`GET` /api/tarefa/{id}

**Exemplo de corpo do response**
| Campo | Tipo | Descrição
|:-----:|:----:|:--------:|
| id | String | Indentificador numérico da tarefa
| title | String | Título da tarefa
| description | String | Descrição da tarefa
| state | int | Estado da tarefa, podendo ser: New, Active, Resolved e Closed
| assignedTo | String | Email corporativo do colaborador responsável pela tarefa
| createdDate | String | Data de criação da tarefa
| activeDate | String | Data que a atividade foi iniciada
| resolvedDate | String | Data que a atividade foi resolvida e aguarda aprovação
| closedDate | String | Data que a atividade foi aprovada e fechada

```json
{
	"id":"4b9226ea-8b9f-4b01-b58a-3887a5b25f61",
	"title":"Criar relatório de atividade da aplicação XPTO",
	"description":"Dado que ... Quando ... Então ...",
	"state":0,
	"createdDate":"2023-04-07T19:00:00",
	"assignedTo":"joao.silva@empresa.com.br",
	"activeDate":"2023-04-07T19:05:00",
	"resolvedDate":"2023-04-08T11:02:30",
	"closedDate":"2023-04-09T13:21:05",
}
```

**Códigos de Resposta**
| Código | Descrição
|:------:|:---------:|
| 200 | Tarefa recuperada com sucesso
| 400 | Erro na requisição
| 404 | Tarefa não encontrada

---
---
---

### Recuperar todas Tarefas
`GET` /api/tarefa/

**Exemplo de corpo do response**
| Campo | Tipo | Descrição
|:-----:|:----:|:--------:|
| id | String | Indentificador numérico da tarefa
| title | String | Título da tarefa
| description | String | Descrição da tarefa
| state | int | Estado da tarefa, podendo ser: 0 = New, 1 - Active, 2 - Resolved e 3 - Closed
| assignedTo | String | Email corporativo do colaborador responsável pela tarefa
| createdDate | String | Data de criação da tarefa
| activeDate | String | Data que a atividade foi iniciada
| resolvedDate | String | Data que a atividade foi resolvida e aguarda aprovação
| closedDate | String | Data que a atividade foi aprovada e fechada

```json
{
	[
		{
			"id":"4b9226ea-8b9f-4b01-b58a-3887a5b25f61",
			"title":"Criar relatório de atividade da aplicação XPTO",
			"description":"Dado que ... Quando ... Então ...",
			"state":2,
			"createdDate":"2023-04-07T19:00:00",
			"assignedTo":"joao.silva@empresa.com.br",
			"activeDate":"2023-04-07T19:05:00",
			"resolvedDate":"2023-04-08T11:02:30",
			"closedDate":"2023-04-09T19:15:42"
		},
		{
			"id":"4b9226ea-8b9f-4b01-b58a-3887a5b25f62",
			"title":"Criar feature de consulta",
			"description":"Dado que ... Quando ... Então ...",
			"state":1,
			"createdDate":"2023-04-07T19:02:00",
			"assignedTo":"sergio.sales@empresa.com.br",
			"activeDate":"2023-04-07T19:12:40",
			"resolvedDate":null,
			"closedDate":null
		}
	]
}
```

**Códigos de Resposta**
| Código | Descrição
|:------:|:---------:|
| 200 | Tarefas recuperadas com sucesso
| 400 | Erro na requisição

---
---
---

### Recuperar todas Tarefas não fechadas
`GET` /api/tarefa/

**Exemplo de corpo do response**
| Campo | Tipo | Descrição
|:-----:|:----:|:--------:|
| id | String | Indentificador numérico da tarefa
| title | String | Título da tarefa
| description | String | Descrição da tarefa
| state | int | Estado da tarefa, podendo ser: New, Active, Resolved e Closed
| assignedTo | String | Email corporativo do colaborador responsável pela tarefa
| createdDate | String | Data de criação da tarefa
| activeDate | String | Data que a atividade foi iniciada
| resolvedDate | String | Data que a atividade foi resolvida e aguarda aprovação
| closedDate | String | Data que a atividade foi aprovada e fechada

```json
{
	[
		{
			"id":"4b9226ea-8b9f-4b01-b58a-3887a5b25f61",
			"title":"Criar relatório de atividade da aplicação XPTO",
			"description":"Dado que ... Quando ... Então ...",
			"state":2,
			"createdDate":"2023-04-07T19:00:00",
			"assignedTo":"joao.silva@empresa.com.br",
			"activeDate":"2023-04-07T19:05:00",
			"resolvedDate":"2023-04-08T11:02:30",
			"closedDate":null
		},
		{
			"id":"2fdc26ko-1b9f-7b91-l55h-1387a5b25abc",
			"title":"Criar feature de consulta",
			"description":"Dado que ... Quando ... Então ...",
			"state":1,
			"createdDate":"2023-04-07T19:02:00",
			"assignedTo":"sergio.sales@empresa.com.br",
			"activeDate":"2023-04-07T19:12:40",
			"resolvedDate":null,
			"closedDate":null
		}
	]
}
``` 03

**Códigos de Resposta**
| Código | Descrição
|:------:|:---------:|
| 200 | Tarefas recuperadas com sucesso
| 400 | Erro na requisição

---
---
---

### Deletar Tarefa
`DELETE` /api/tarefa/{id}

**Códigos de Resposta**
| Código | Descrição
|:------:|:---------:|
| 204 | Tarefa deletada com sucesso
| 404 | Tarefa não encontrada

---