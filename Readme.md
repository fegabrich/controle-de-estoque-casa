# Controle de Estoque em C#

Este projeto é um **Sistema de controle de Estoque** que estive desenvolvendo em C# para aplicações de console e me senti apta a postar. Ele permite adicionar, remover, atualizar e listar itens, salvando os dados em um arquivo. Continue lendo para entender como ele funciona!

## Funcionalidades

**Adicionar item:** Permite inserir um novo produto no estoque, informando nome, quantidade, categoria e validade. Se o item já existir, é possível apenas atualizar a quantidade.  
**Remover item:** Remove um item do estoque pelo nome.  
**Atualizar quantidade:** Atualiza a quantidade de um item existente no estoque.  
**Listar itens:** Exibe todos os itens cadastrados, mostrando nome, quantidade, categoria e validade.  
**Persistência com CSV:** Os dados são salvos em um arquivo `estoque.csv` no formato de valores separados por vírgula, mantendo o estoque mesmo após fechar o programa.

## Estrutura do projeto

**Program.cs:** Contém todas as funções do sistema e o menu interativo.  
**Item.cs:** Classe que representa um item do estoque, com os atributos:
  - `nome` → Nome do produto.  
  - `quantidade` → Quantidade disponível no estoque.  
  - `categoria` → Categoria do produto (ex.: Alimentos, Eletrônicos).  
  - `validade` → Data de validade do item.  

**estoque.csv:** Arquivo gerado automaticamente para armazenar os dados do estoque. 

Estrutura pedida ao usuário: nome,quantidade,categoria,validade

Exemplo de entrada de dados: Arroz,10,Alimentos,15/12/2025; 
Teclado,5,Eletrônicos,01/01/2026
