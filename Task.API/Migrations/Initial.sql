DROP SCHEMA IF EXISTS public CASCADE;
CREATE SCHEMA IF NOT EXISTS public;

DROP SCHEMA IF EXISTS enum CASCADE;
CREATE SCHEMA IF NOT EXISTS enum;

-- Extension initialization.
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE enum.task_type
(
	task_type_id INT GENERATED ALWAYS AS IDENTITY 
		CONSTRAINT etti_pkey PRIMARY KEY,

	"name" TEXT NOT NULL,

	created_at TIMESTAMPTZ NOT NULL
		DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE enum.task_status
(
	task_status_id INT GENERATED ALWAYS AS IDENTITY 
		CONSTRAINT etsi_pkey PRIMARY KEY,

	"name" TEXT NOT NULL,

	created_at TIMESTAMPTZ NOT NULL
		DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE task
(
	"task_id"	UUID
			CONSTRAINT ti_pkey PRIMARY KEY,

	"user_id"	UUID NOT NULL,

	task_type_id INT NOT NULL
			CONSTRAINT t_tti_fkey REFERENCES enum.task_type,

	task_status_id INT NOT NULL
			CONSTRAINT t_tsi_fkey REFERENCES enum.task_status,

	"title"	VARCHAR(255) NOT NULL,
	"description"	TEXT NOT NULL,

	"planned_start" TIMESTAMPTZ NOT NULL,

	"planned_end" TIMESTAMPTZ NOT NULL,	

	"start_date" TIMESTAMPTZ NULL,

	"end_date" TIMESTAMPTZ NULL,

	created_at	TIMESTAMPTZ NOT NULL
				DEFAULT CURRENT_TIMESTAMP	
);