clean:
	cargo clean

build:
	cargo build

run:
	cargo run

start_deploy:
	docker build -t rabbitmq_consummer_template

all_down_deploy:
	docker builder prune -a -f