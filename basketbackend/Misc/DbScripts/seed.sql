\connect basketdb

CREATE TABLE basket
(
    id serial PRIMARY KEY,
    customer  VARCHAR (100)  NOT NULL,
    paysVat BOOLEAN default true,
    paid boolean default false,
    closed boolean default false
);

ALTER TABLE "basket" OWNER TO someuser;
Insert into basket(customer, paysvat) values('Basket 1', true);
Insert into basket(customer, paysvat) values('Basket 2', false);

CREATE TABLE item
(
    id serial PRIMARY KEY,
    name  VARCHAR (200)  NOT NULL,
    price FLOAT,
    basket_id integer REFERENCES basket
);

ALTER TABLE "item" OWNER TO someuser;
Insert into item(name, price, basket_id) values('apple', 20.0, 1);
Insert into item(name, price, basket_id) values('orange', 1.5, 1);
Insert into item(name, price, basket_id) values('apple', 20.0, 2);
Insert into item(name, price, basket_id) values('juice', 11.3, 1);

CREATE USER postgres WITH PASSWORD 'postgres';