-- DROP TABLE IF EXISTS Users, Roles, Clients, Administrators, Moderators, PrizeDrawsClients, PrizeDraws, Questions, Orders, Offers, Apartments, ApartmentsTypes, ApartmentsClasses, Logs, LogsTypes;

CREATE TABLE Roles(
	id uuid PRIMARY KEY,
	role_name varchar(50) NOT NULL
);

CREATE TABLE Users(
	id uuid PRIMARY KEY,
	login varchar(100) NOT NULL,
	password varchar(100) NOT NULL,
	first_name varchar(100) NOT NULL,
	last_name varchar(100) NOT NULL,
	fk_role_id uuid REFERENCES Roles(id) NOT NULL
);

CREATE INDEX user_id_index ON Users (id);

CREATE TABLE Clients(
	id uuid PRIMARY KEY REFERENCES Users(id),
	patronymic varchar(100) NOT NULL,
	date_of_birth date NOT NULL
);

CREATE TABLE Administrators(
	id uuid PRIMARY KEY REFERENCES Users(id)
);

CREATE TABLE Moderators(
	id uuid PRIMARY KEY REFERENCES Users(id)
);

CREATE TABLE PrizeDraws(
	id uuid PRIMARY KEY,
	amount_of_money float8 NOT NULL
);

CREATE TABLE PrizeDrawsClients(
	prize_draw_id uuid REFERENCES PrizeDraws(id) NOT NULL,
	client_id uuid REFERENCES Clients(id) NOT NULL
);

CREATE TABLE Questions(
	id uuid PRIMARY KEY,
	client_id uuid REFERENCES Clients(id) NOT NULL,
	question varchar(1000) NOT NULL,
	answer varchar(1000) NOT NULL,
	status boolean NOT NULL
);

CREATE TABLE ApartmentsTypes(
	id uuid PRIMARY KEY,
	apartments_type_name varchar(50) NOT NULL
);

CREATE TABLE ApartmentsClasses(
	id uuid PRIMARY KEY,
	apartments_class_name varchar(50) NOT NULL
);

CREATE TABLE Apartments(
	id uuid PRIMARY KEY,
	apartments_type_id uuid REFERENCES ApartmentsTypes(id) NOT NULL,
	count_rooms int NOT NULL,
	count_floors int NOT NULL,
	count_sleeping_places int NOT NULL,
	apartments_class_id uuid REFERENCES ApartmentsClasses(id) NOT NULL
);

CREATE TABLE Offers(
	id uuid PRIMARY KEY,
	apartments_id uuid REFERENCES Apartments(id) NOT NULL,
	cost_for_day float8 NOT NULL,
	days int NOT NULL,
	country varchar(100) NOT NULL,
	address varchar(100) NOT NULL
);

CREATE TABLE Orders(
	id uuid PRIMARY KEY,
	client_id uuid REFERENCES Clients(id) NOT NULL,
	offer_id uuid REFERENCES Offers(id) NOT NULL,
	final_price float8 NOT NULL,
	arriving_date date NOT NULL,
	departure_date date NOT NULL
);

CREATE INDEX client_id_index ON Orders (client_id);

CREATE TABLE LogsTypes(
	id uuid PRIMARY KEY,
	log_type_name varchar(1000) NOT NULL
);

CREATE TABLE Logs(
	id uuid PRIMARY KEY,
	log_type_id uuid REFERENCES LogsTypes(id) NOT NULL,
	happened_at timestamp(6) NOT NULL, -- need to add to scheme
	client_id uuid NOT NULL -- need to add to scheme
);