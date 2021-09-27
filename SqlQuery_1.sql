-- DROP TABLE universities;

--PostgreSQL
CREATE TABLE universities
(
    country character varying(250) NOT NULL,
    name character varying(250) NOT NULL,
    alpha_two_code character varying(250) NOT NULL,
    state_province boolean NOT NULL,
    domains character varying(250) NOT NULL,
    web_pages character varying(250) NOT NULL
)

--MSSQL
CREATE TABLE universities
(
    country character nvarchar(250) NOT NULL,
    name character nvarchar(250) NOT NULL,
    alpha_two_code character nvarchar(250) NOT NULL,
    state_province boolean NOT NULL,
    domains character nvarchar(250) NOT NULL,
    web_pages character nvarchar(250) NOT NULL
)
