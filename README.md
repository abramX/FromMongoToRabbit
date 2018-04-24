# FromMongoToRabbit
The Project uses MongoDB, RabbitMQ, TopShelf and Quartz:
Every minute(QUartz) the Producers Service(TopShelf) reads from a MongoDb Collection and send the datas to the Consumer Services,
through RabbitMQ, and write the datas in another Mongo db.
