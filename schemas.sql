CREATE TABLE IF NOT EXISTS public.customers
(
    id SERIAL NOT NULL,
    name character varying(255) NOT NULL,
    CONSTRAINT customers_pk PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS public.vehicle (
    id SERIAL NOT NULL,
    model character varying(255),
    vin character varying(255),
    color character varying(255),
    engine character varying(255),
    year integer,
    gearbox character varying(255),
    axle_gear character varying(255),
    rear_axle_gear_ratio numeric(2,2),
    customer_id integer,
    plate character varying(20) NOT NULL,
    type character varying(255),
    CONSTRAINT vehicle_pkey PRIMARY KEY (id),
    FOREIGN KEY (customer_id) REFERENCES customers(id)
    ON UPDATE NO ACTION
    ON DELETE NO ACTION
);

CREATE TABLE IF NOT EXISTS public.truck_orders (
    id SERIAL NOT NULL PRIMARY KEY,
    fid integer NOT NULL UNIQUE,
    date_field date NOT NULL,
    customer_id integer NOT NULL,
    description VARCHAR(2000),
    iswarranty boolean NOT NULL,
    kilometers integer NOT NULL,
    isarchived boolean NOT NULL,
    storedvolume integer NOT NULL,
    vehicle_id integer,
    FOREIGN KEY (customer_id) REFERENCES customers(id)
    ON UPDATE NO ACTION
    ON DELETE NO ACTION,
    FOREIGN KEY (vehicle_id) REFERENCES vehicle(id)
    ON UPDATE NO ACTION
    ON DELETE NO ACTION
);

CREATE TABLE IF NOT EXISTS public.bus_orders
(
    id SERIAL NOT NULL PRIMARY KEY,
    fid integer NOT NULL UNIQUE,
    date_field date NOT NULL,
    customer_id integer NOT NULL,
    description VARCHAR(2000),
    iswarranty boolean NOT NULL,
    kilometers integer NOT NULL,
    isarchived boolean NOT NULL,
    storedvolume integer NOT NULL,
    vehicle_id integer,
    FOREIGN KEY (customer_id) REFERENCES customers(id)
    ON UPDATE NO ACTION
    ON DELETE NO ACTION,
    FOREIGN KEY (vehicle_id) REFERENCES vehicle(id)
    ON UPDATE NO ACTION
    ON DELETE NO ACTION
);

CREATE TABLE IF NOT EXISTS public.mechanics
(
    id SERIAL NOT NULL PRIMARY KEY,
    name character varying(255) NOT NULL,
    status boolean NOT NULL,
    created_date date NOT NULL
);

CREATE TABLE IF NOT EXISTS public.mechanics_orders
(
    id SERIAL NOT NULL PRIMARY KEY,
    work_order_id integer NOT NULL,
    work_order_type character varying(255) NOT NULL,
    mechanic_id integer NOT NULL,
    FOREIGN KEY (mechanic_id) REFERENCES mechanics(id)
    ON UPDATE NO ACTION
    ON DELETE NO ACTION
);

INSERT INTO public.vehicle(
	id, model, vin, color, engine, year, gearbox, axle_gear, rear_axle_gear_ratio, plate, type)
	VALUES (DEFAULT, 'generic', 'generic', 'generic', 'generic', 2000, 'generic', 'generic', null, 'generic', 'generic');