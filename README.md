*** Which repositories should I clone? ***
----------------------------------------------------------------------------------------------------

Please clone the following repositories and put them into the same working directory:

- [Ookbee-Ads]

*** How to start the solution? ***
----------------------------------------------------------------------------------------------------

At first, you need to have the following services up and running on localhost (so-called bare minimum):
- [MongoDB](https://www.mongodb.com)
- [PostgreSQL](https://www.postgresql.org)
- [RabbitMQ](https://www.rabbitmq.com)
- [Redis](https://redis.io)

*** How to start the solution? ***
----------------------------------------------------------------------------------------------------

These can be run as standalone services, or via Docker (recommended approach). 
You can run them one by one e.g.

docker run --name mongo -d -p 27017:27017 -e MONGO_INITDB_ROOT_USERNAME=admin -e MONGO_INITDB_ROOT_PASSWORD=pwd1234 -e MONGO_INITDB_DATABASE=ookbee-ads mongo

docker run --name postgres -d -p 5432:5432 -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=pwd1234 -e POSTGRES_DB=ookbee-ads postgres 

docker run --name rabbitmq -d -p 5672:5672 -p 15672:15672 --hostname rabbitmq rabbitmq:3-management

docker run --name redis -d -p 6379:6379-e REDIS_PASSWORD=pwd1234 redis

Or using Docker compose (first, create a new `docker-compose.yml` file and then execute `docker-compose up` command):
You can also find this file `docker-compose-mongo-postgres-rabbit-redis.yml`, which includes custom network and volumes. 
In order to start it, execute `docker-compose -f ` up -d` (-d will run containers in the background).

docker-compose -f docker-compose-mongo-postgres-rabbit-redis.yml up -d --force-recreate
docker-compose -f docker-compose-mongo-postgres-rabbit-redis.yml down --remove-orphans --volumes

If you want to start additional infrastructural services e.g. Consul, Fabio and Vault, execute `docker-compose-consul-fabio-rabbit-vault.yml up -d` command.

docker-compose -f docker-compose-consul-fabio-vault.yml up -d --force-recreate
docker-compose -f docker-compose-consul-fabio-vault.yml down --remove-orphans --volumes

Restart All

docker restart $(docker ps -a -q)

To start only stopped containers

docker start $(docker ps -a -q -f status=exited)

consul management system, default UI is accessible at http://localhost:8500
RabbitMQ management system, default UI is accessible at http://localhost:15672

*** What HTTP requests can be sent to the API? ***
----------------------------------------------------------------------------------------------------

You can find the list of all HTTP requests in `DShop.rest` file placed in the root folder of [Ookbee.Ads]
This file is compatible with [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) plugin for [Visual Studio Code](https://code.visualstudio.com).
