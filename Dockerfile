FROM rust:latest as builder

RUN rustup target add x86_64-unknown-linux-musl

RUN apt-get update && apt-get install -y musl-tools && rm -rf /var/lib/apt/lists/*

WORKDIR /app

COPY Cargo.toml .
RUN mkdir src && echo "fn main() {}" > src/main.rs

RUN cargo build --release --target x86_64-unknown-linux-musl || true

COPY . .

RUN cargo build --release --target x86_64-unknown-linux-musl

FROM alpine:latest

RUN apk add --no-cache ca-certificates

COPY --from=builder /app/target/x86_64-unknown-linux-musl/release/rabbitmq_consummer_template /usr/local/bin/rabbitmq_consummer_template

CMD ["rabbitmq_consummer_template"]