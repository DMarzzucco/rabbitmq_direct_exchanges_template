use futures_lite::stream::StreamExt;
use lapin::{Connection, ConnectionProperties, options::*, types::FieldTable};
use serde::Deserialize;
use serde_json;

#[derive(Debug, Deserialize)]
struct ModelMessageExample {
    email: String,
    token: String,
}

#[tokio::main]
async fn main() {

    let addr = "amqp://user:password@host:5672/%2f";
    let conn = Connection::connect(addr, ConnectionProperties::default())
        .await
        .expect("❌ not could connecto to RabbitMQ");

    let channel = conn
        .create_channel()
        .await
        .expect("❌ no could create a channel");

    channel
        .queue_declare(
            "queue_name",
            QueueDeclareOptions::default(),
            FieldTable::default(),
        )
        .await
        .expect("❌ no could declarte the queue");

    let mut consumer = channel
        .basic_consume(
            "queue_name",
            "template_example_wroker",
            BasicConsumeOptions::default(),
            FieldTable::default(),
        )
        .await
        .expect("❌ no could consummer the queue");

    println!("🐇 wait for the message...");

    while let Some(result) = consumer.next().await {
        if let Ok(delivery) = result {
            let body = &delivery.data;
            match serde_json::from_slice::<ModelMessageExample>(body) {
                Ok(msg) => {
                    println!(
                        "⛰ Welcome {}, your token is: {}",
                        msg.email, msg.token
                    );
                }
                Err(e) => {
                    eprintln!("❌ Error to deserialize message: {:?}", e);
                }
            }

            delivery.ack(BasicAckOptions::default()).await.unwrap();
        }
    }
}
