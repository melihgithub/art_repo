-- PostgreSQL Database Creation Script for Artist Portfolio

-- Create database (run this separately)
-- CREATE DATABASE artist_portfolio;

-- Connect to the database and run the following:

-- Users table
CREATE TABLE "Users" (
    "Id" SERIAL PRIMARY KEY,
    "Username" VARCHAR(50) NOT NULL UNIQUE,
    "Email" VARCHAR(100) NOT NULL UNIQUE,
    "PasswordHash" TEXT NOT NULL,
    "FullName" VARCHAR(100),
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "LastLoginAt" TIMESTAMP,
    "IsActive" BOOLEAN DEFAULT TRUE
);

-- About table
CREATE TABLE "Abouts" (
    "Id" SERIAL PRIMARY KEY,
    "Title" VARCHAR(200) NOT NULL,
    "Content" TEXT NOT NULL,
    "ImageUrl" VARCHAR(500),
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "IsActive" BOOLEAN DEFAULT TRUE
);

-- NewsItems table
CREATE TABLE "NewsItems" (
    "Id" SERIAL PRIMARY KEY,
    "Title" VARCHAR(200) NOT NULL,
    "Content" TEXT NOT NULL,
    "ImageUrl" VARCHAR(500),
    "VideoUrl" VARCHAR(500),
    "Type" INTEGER NOT NULL, -- 1: Program, 2: Interview, 3: Magazine, 4: Newspaper
    "PublishedDate" TIMESTAMP NOT NULL,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "IsActive" BOOLEAN DEFAULT TRUE
);

-- Artworks table
CREATE TABLE "Artworks" (
    "Id" SERIAL PRIMARY KEY,
    "Title" VARCHAR(200) NOT NULL,
    "Description" TEXT,
    "ImageUrl" VARCHAR(500) NOT NULL,
    "Technique" VARCHAR(100),
    "Dimensions" VARCHAR(100),
    "Year" INTEGER,
    "Price" DECIMAL(18,2),
    "IsForSale" BOOLEAN DEFAULT FALSE,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "IsActive" BOOLEAN DEFAULT TRUE,
    "DisplayOrder" INTEGER DEFAULT 0
);

-- Exhibitions table
CREATE TABLE "Exhibitions" (
    "Id" SERIAL PRIMARY KEY,
    "Title" VARCHAR(200) NOT NULL,
    "Description" TEXT,
    "Location" VARCHAR(200),
    "StartDate" TIMESTAMP NOT NULL,
    "EndDate" TIMESTAMP,
    "ImageUrl" VARCHAR(500),
    "Type" INTEGER NOT NULL, -- 1: Solo, 2: Group, 3: Participation
    "Status" INTEGER NOT NULL, -- 1: Upcoming, 2: Active, 3: Past
    "IsInternational" BOOLEAN DEFAULT FALSE,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "IsActive" BOOLEAN DEFAULT TRUE
);

-- Workshops table
CREATE TABLE "Workshops" (
    "Id" SERIAL PRIMARY KEY,
    "Title" VARCHAR(200) NOT NULL,
    "Description" TEXT,
    "ImageUrl" VARCHAR(500),
    "Type" INTEGER NOT NULL, -- 1: About, 2: StudentWork, 3: Process
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "IsActive" BOOLEAN DEFAULT TRUE
);

-- ContactMessages table
CREATE TABLE "ContactMessages" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "Email" VARCHAR(100) NOT NULL,
    "Phone" VARCHAR(20),
    "Subject" VARCHAR(200) NOT NULL,
    "Message" TEXT NOT NULL,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "IsRead" BOOLEAN DEFAULT FALSE
);

-- FAQs table
CREATE TABLE "FAQs" (
    "Id" SERIAL PRIMARY KEY,
    "Question" VARCHAR(300) NOT NULL,
    "Answer" TEXT NOT NULL,
    "DisplayOrder" INTEGER DEFAULT 0,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UpdatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "IsActive" BOOLEAN DEFAULT TRUE
);

-- Create indexes for better performance
CREATE INDEX "IX_NewsItems_Type" ON "NewsItems" ("Type");
CREATE INDEX "IX_NewsItems_PublishedDate" ON "NewsItems" ("PublishedDate");
CREATE INDEX "IX_Artworks_DisplayOrder" ON "Artworks" ("DisplayOrder");
CREATE INDEX "IX_Exhibitions_StartDate" ON "Exhibitions" ("StartDate");
CREATE INDEX "IX_Exhibitions_Status" ON "Exhibitions" ("Status");
CREATE INDEX "IX_Exhibitions_IsInternational" ON "Exhibitions" ("IsInternational");
CREATE INDEX "IX_Workshops_Type" ON "Workshops" ("Type");
CREATE INDEX "IX_ContactMessages_CreatedAt" ON "ContactMessages" ("CreatedAt");
CREATE INDEX "IX_ContactMessages_IsRead" ON "ContactMessages" ("IsRead");
CREATE INDEX "IX_FAQs_DisplayOrder" ON "FAQs" ("DisplayOrder");

-- Insert sample admin user (password: Admin123!)
-- Note: In production, use proper password hashing
INSERT INTO "Users" ("Username", "Email", "PasswordHash", "FullName", "IsActive") 
VALUES ('admin', 'admin@artistportfolio.com', '$2a$11$8K1p/a0dqbQupuLODU2LO.ZGmZkiH6.Sq7xIwjxeOFvxsaQQXdTHu', 'Admin User', TRUE);

-- Insert sample data
INSERT INTO "Abouts" ("Title", "Content", "IsActive") 
VALUES ('Hakkımda', 'Ben bir ressamım ve sanatla hayatımı şekillendiriyorum. Yıllardır çeşitli tekniklerle eserler üretiyorum.', TRUE);

INSERT INTO "FAQs" ("Question", "Answer", "DisplayOrder", "IsActive") 
VALUES 
('Eserlerinizi nasıl satın alabilirim?', 'İletişim sayfasından benimle iletişime geçebilirsiniz.', 1, TRUE),
('Atölye dersleri veriyor musunuz?', 'Evet, düzenli olarak atölye dersleri veriyorum.', 2, TRUE);