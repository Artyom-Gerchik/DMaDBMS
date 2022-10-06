CREATE TABLE "Users"(
    "id" UUID NOT NULL,
    "login" VARCHAR(255) NOT NULL,
    "password" VARCHAR(255) NOT NULL,
    "first_name" VARCHAR(255) NOT NULL,
    "last_name" VARCHAR(255) NOT NULL,
    "role_id" UUID NOT NULL
);
ALTER TABLE
    "Users" ADD PRIMARY KEY("id");
CREATE TABLE "Clients"(
    "id" UUID NOT NULL,
    "patronymic" VARCHAR(255) NOT NULL,
    "date_of_birth" DATE NOT NULL
);
ALTER TABLE
    "Clients" ADD PRIMARY KEY("id");
CREATE TABLE "Moderators"("id" UUID NOT NULL);
ALTER TABLE
    "Moderators" ADD PRIMARY KEY("id");
CREATE TABLE "Orders"(
    "id" UUID NOT NULL,
    "client_id" UUID NOT NULL,
    "offer_id" UUID NOT NULL,
    "arriving_date" DATE NOT NULL,
    "final_price" DOUBLE PRECISION NOT NULL
);
ALTER TABLE
    "Orders" ADD PRIMARY KEY("id");
CREATE TABLE "Questions"(
    "id" UUID NOT NULL,
    "client_id" UUID NOT NULL,
    "question" VARCHAR(255) NOT NULL,
    "answer" VARCHAR(255) NOT NULL,
    "status" BOOLEAN NOT NULL
);
ALTER TABLE
    "Questions" ADD PRIMARY KEY("id");
CREATE TABLE "Administrators"("id" UUID NOT NULL);
ALTER TABLE
    "Administrators" ADD PRIMARY KEY("id");
CREATE TABLE "Apartments"(
    "id" UUID NOT NULL,
    "apartments_type_id" UUID NOT NULL,
    "count_rooms" INTEGER NOT NULL,
    "count_floors" INTEGER NOT NULL,
    "count_sleeping_places" INTEGER NOT NULL,
    "apartments_class_id" UUID NOT NULL
);
ALTER TABLE
    "Apartments" ADD PRIMARY KEY("id");
CREATE TABLE "Offers"(
    "id" UUID NOT NULL,
    "appartments_id" UUID NOT NULL,
    "cost_for_day" DOUBLE PRECISION NOT NULL,
    "days" INTEGER NOT NULL,
    "country" VARCHAR(255) NOT NULL,
    "address" VARCHAR(255) NOT NULL
);
ALTER TABLE
    "Offers" ADD PRIMARY KEY("id");
CREATE TABLE "OrdersLogs"(
    "id" UUID NOT NULL,
    "order_id" UUID NOT NULL,
    "date_of_order" INTEGER NOT NULL
);
ALTER TABLE
    "OrdersLogs" ADD PRIMARY KEY("id");
CREATE TABLE "ApartmentsTypes"(
    "id" UUID NOT NULL,
    "apartments_type_name" VARCHAR(255) NOT NULL
);
ALTER TABLE
    "ApartmentsTypes" ADD PRIMARY KEY("id");
CREATE TABLE "ApartmentsClasses"(
    "id" UUID NOT NULL,
    "apartments_class_name" VARCHAR(255) NOT NULL
);
ALTER TABLE
    "ApartmentsClasses" ADD PRIMARY KEY("id");
CREATE TABLE "Roles"(
    "id" UUID NOT NULL,
    "role_name" VARCHAR(255) NOT NULL
);
ALTER TABLE
    "Roles" ADD PRIMARY KEY("id");
CREATE TABLE "QuestionsLogs"(
    "id" UUID NOT NULL,
    "question_id" UUID NOT NULL
);
ALTER TABLE
    "QuestionsLogs" ADD PRIMARY KEY("id");
CREATE TABLE "PrizeDraw"(
    "id" UUID NOT NULL,
    "amount_of_money" DOUBLE PRECISION NOT NULL
);
ALTER TABLE
    "PrizeDraw" ADD PRIMARY KEY("id");
CREATE TABLE "PrizeDrawClients"(
    "prize_draw_id" UUID NOT NULL,
    "client_id" UUID NOT NULL
);
ALTER TABLE
    "PrizeDrawClients" ADD PRIMARY KEY("prize_draw_id");
ALTER TABLE
    "PrizeDrawClients" ADD PRIMARY KEY("client_id");
ALTER TABLE
    "Orders" ADD CONSTRAINT "orders_client_id_foreign" FOREIGN KEY("client_id") REFERENCES "Clients"("id");
ALTER TABLE
    "Users" ADD CONSTRAINT "users_role_id_foreign" FOREIGN KEY("role_id") REFERENCES "Roles"("id");
ALTER TABLE
    "Orders" ADD CONSTRAINT "orders_offer_id_foreign" FOREIGN KEY("offer_id") REFERENCES "Offers"("id");
ALTER TABLE
    "Questions" ADD CONSTRAINT "questions_client_id_foreign" FOREIGN KEY("client_id") REFERENCES "Clients"("id");
ALTER TABLE
    "Apartments" ADD CONSTRAINT "apartments_apartments_type_id_foreign" FOREIGN KEY("apartments_type_id") REFERENCES "ApartmentsTypes"("id");
ALTER TABLE
    "Apartments" ADD CONSTRAINT "apartments_apartments_class_id_foreign" FOREIGN KEY("apartments_class_id") REFERENCES "ApartmentsClasses"("id");
ALTER TABLE
    "Offers" ADD CONSTRAINT "offers_appartments_id_foreign" FOREIGN KEY("appartments_id") REFERENCES "Apartments"("id");
ALTER TABLE
    "OrdersLogs" ADD CONSTRAINT "orderslogs_order_id_foreign" FOREIGN KEY("order_id") REFERENCES "Orders"("id");
ALTER TABLE
    "QuestionsLogs" ADD CONSTRAINT "questionslogs_question_id_foreign" FOREIGN KEY("question_id") REFERENCES "Questions"("id");