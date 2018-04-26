# FromMongoToRabbit
The Project uses MongoDB, RabbitMQ, TopShelf and Quartz:
Every minute(Quartz) the Producer Service(TopShelf) reads from a MongoDb Product Collection and send the datas to the Consumer Services,
through RabbitMQ, and write the datas in another Mongo db Product Collection.
