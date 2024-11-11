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

-- This table is not used in the current version 10/09/24

-- CREATE TABLE IF NOT EXISTS public.truck_orders (
--     id SERIAL NOT NULL PRIMARY KEY,
--     fid integer NOT NULL UNIQUE,
--     date_field date NOT NULL,
--     customer_id integer NOT NULL,
--     description VARCHAR(2000),
--     iswarranty boolean NOT NULL,
--     kilometers integer NOT NULL,
--     isarchived boolean NOT NULL,
--     storedvolume integer NOT NULL,
--     vehicle_id integer,
--     FOREIGN KEY (customer_id) REFERENCES customers(id)
--     ON UPDATE NO ACTION
--     ON DELETE NO ACTION,
--     FOREIGN KEY (vehicle_id) REFERENCES vehicle(id)
--     ON UPDATE NO ACTION
--     ON DELETE NO ACTION
-- );

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

-- 24-06-27 UPDATE, YOU NEED TO ADD THIS SQL SCRIPT TO YOUR DATABASE TO MAKE IT WORK. IF YOU'RE STARTING THE DB WITHOUT DATA IGNORE THIS MESSAGE

------------ 24-06-27 UPDATE STARTS HERE ----------
CREATE TABLE IF NOT EXISTS public.notices
(
    id SERIAL NOT NULL PRIMARY KEY,
    vehicle_id integer NOT NULL,
    notice_date date NOT NULL,
    description VARCHAR(2000),
    severity character varying(50),
    resolved boolean NOT NULL DEFAULT FALSE,
    FOREIGN KEY (vehicle_id) REFERENCES vehicle(id)
    ON UPDATE NO ACTION
    ON DELETE NO ACTION
);
------------ 24-06-27 UPDATE ENDS HERE ----------

----------- 24-07-14 UPDATE STARTS HERE ----------

CREATE TABLE IF NOT EXISTS public.maintenance_registry
(
    id SERIAL NOT NULL PRIMARY KEY,
    vehicle_fk_id integer NOT NULL,
    order_fk_id integer NOT NULL,
    maintenance_date date NOT NULL,
    maintenance_type VARCHAR(50) NOT NULL,
    description VARCHAR(255) NOT NULL,
    kilometers integer NOT NULL,
    FOREIGN KEY (vehicle_fk_id) REFERENCES vehicle(id)
    ON UPDATE CASCADE
    ON DELETE CASCADE,
    FOREIGN KEY (order_fk_id) REFERENCES bus_orders(id)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

-- ADD COLUMN TO bus_orders TABLE
ALTER TABLE public.bus_orders
ADD COLUMN vehicle_type character varying(255) default 'bus';

-- REMOVE CONSTRAINT FROM bus_orders TABLE
ALTER TABLE public.bus_orders
DROP CONSTRAINT bus_orders_fid_key;

----------- 24-07-14 UPDATE ENDS HERE ----------

----------- 24-07-15 UPDATE STARTS HERE ----------
-- Eliminar las restricciones de clave externa existentes
ALTER TABLE public.maintenance_registry
DROP CONSTRAINT maintenance_registry_vehicle_fk_id_fkey;

ALTER TABLE public.maintenance_registry
DROP CONSTRAINT maintenance_registry_order_fk_id_fkey;

-- Agregar las nuevas restricciones de clave externa con CASCADE
ALTER TABLE public.maintenance_registry
ADD CONSTRAINT maintenance_registry_vehicle_fk_id_fkey FOREIGN KEY (vehicle_fk_id) 
REFERENCES vehicle(id) 
ON UPDATE CASCADE
ON DELETE NO ACTION;

ALTER TABLE public.maintenance_registry
ADD CONSTRAINT maintenance_registry_order_fk_id_fkey FOREIGN KEY (order_fk_id) 
REFERENCES bus_orders(id) 
ON UPDATE CASCADE
ON DELETE CASCADE;
----------- 24-07-15 UPDATE ENDS HERE ----------
----------- 10-09-24 UPDATE STARTS HERE ----------

ALTER TABLE public.customers
ADD COLUMN id_ruc_number character varying(20),
ADD COLUMN phone_number character varying(15);

ALTER TABLE public.vehicle
ADD COLUMN maintenance_agreement character varying(255);

----------- 10-09-24 UPDATE ENDS HERE ----------

----------- 24-09-24 UPDATE STARTS HERE ----------

-- Modificar la columna maintenance_agreement a boolean en la tabla Vehicle
ALTER TABLE public.vehicle
ALTER COLUMN maintenance_agreement TYPE boolean
USING maintenance_agreement::boolean;

-- Agregar nuevas columnas a la tabla Vehicle
ALTER TABLE public.vehicle
ADD COLUMN CooperativeId integer,
ADD COLUMN fleet_number integer;

-- Crear la tabla Cooperatives
CREATE TABLE IF NOT EXISTS public.cooperatives
(
    id SERIAL NOT NULL PRIMARY KEY,
    name character varying(255) NOT NULL,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    city character varying(255) NOT NULL
);

-- Agregar la relación de clave externa para CooperativeId en la tabla Vehicle
ALTER TABLE public.vehicle
ADD CONSTRAINT vehicle_cooperative_fk FOREIGN KEY (CooperativeId) 
REFERENCES cooperatives(id)
ON UPDATE CASCADE
ON DELETE SET NULL;

----------- 24-09-24 UPDATE ENDS HERE ----------

----------- 06-11-24 UPDATE STARTS HERE ----------

-- Crear la tabla 'spare_order'
CREATE TABLE IF NOT EXISTS public.spare_order
(
    id SERIAL NOT NULL PRIMARY KEY,
    bus_order_fk integer,
    customer_fk integer,
    isClosed boolean NOT NULL,
	created_at timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    closed_at timestamp with time zone,
    FOREIGN KEY (bus_order_fk) REFERENCES public.bus_orders(id)
    ON UPDATE NO ACTION
    ON DELETE NO ACTION,
    FOREIGN KEY (customer_fk) REFERENCES public.customers(id)
    ON UPDATE NO ACTION
    ON DELETE NO ACTION
);

-- Crear la tabla 'spare_part'
CREATE TABLE IF NOT EXISTS public.spare_part
(
    id SERIAL NOT NULL PRIMARY KEY,
    code character varying(255) NOT NULL,
    name character varying(255) NOT NULL
);

-- Crear la tabla 'spare_register'
CREATE TABLE IF NOT EXISTS public.spare_register
(
    id SERIAL NOT NULL PRIMARY KEY,
    spare_order_fk integer NOT NULL,
    spare_fk integer NOT NULL,
    quantity integer NOT NULL,
	added_at timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (spare_order_fk) REFERENCES public.spare_order(id)
    ON UPDATE NO ACTION
    ON DELETE NO ACTION,
    FOREIGN KEY (spare_fk) REFERENCES public.spare_part(id)
    ON UPDATE NO ACTION
    ON DELETE NO ACTION
);

----------- 06-11-24 UPDATE ENDS HERE ----------