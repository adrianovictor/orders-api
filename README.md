# Criando a imagem para o container da API

Para criar a imagem, acesse o diretório <strong><i>/src</i><strong> para ter acesso ao arquivo Dockerfile e execute o seguinte comando:

```_> docker build -t ordersservice .```

Aguarde o processo ser concluído. Para testar a imagem criada, execute o seguinte comando:

```_> docker run -p 5296:5000 ordersservice```

Abra o navegador e acesse o endereço: http://localhost:5225/swagger

<br><br>
## Usando o Docker Compsoe para subor o banco e a aplicação

Para subir tanto a api como o banco de dados MongoDB, basta acessar a pasta raiz da aplicação onde se encontra o arquivo docker-compose.yaml e digitar o seguinte comando:

```_> docker compose up```

Aguarde até o fim do processo de iniciação dos serviços para testar.