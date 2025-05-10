-- ip address lookup information for analytics
CREATE TABLE IF NOT EXISTS public.ip_address_lookups
(
    id              UUID                        NOT NULL CONSTRAINT pk_ip_address_lookups PRIMARY KEY,
    ip_address      VARCHAR(52)                 NOT NULL,
    date            TIMESTAMP    NOT NULL,
    country_name    VARCHAR(255),
    country_code    VARCHAR(10),
    city            VARCHAR(255),
    region          VARCHAR(255),
    region_code     VARCHAR(255),
    continent       VARCHAR(50),
    latitude        FLOAT,
    longitude       FLOAT
);

CREATE INDEX IF NOT EXISTS ix_ip_address_lookups_city_grouping ON public.ip_address_lookups (date, country_code, city);
