# example-csharp-app

DB sql

// pgsql

-- Вставка
CREATE PROCEDURE "sp_InsertBook"(
	Id UUID,
    Title VARCHAR(255),
    Author VARCHAR(255),
    YearPublished TimestampTz = NULL,
    BookIndex XML = NULL
)
LANGUAGE plpgsql
as $$
BEGIN   
    INSERT INTO "Books" ("Id", "Title", "Author", "YearPublished", "BookIndex")
    VALUES (Id, Title, Author, YearPublished, BookIndex);
    
end
$$

-- Обновление
CREATE PROCEDURE "sp_UpdateBook"(
	Id UUID,
    Title VARCHAR(255),
    Author VARCHAR(255),
    YearPublished TimestampTz,
    BookIndex xml
)
LANGUAGE plpgsql
as $$
BEGIN   
    UPDATE "Books"
    SET 
        "Title" = Title,
        "Author" = Author,
        "YearPublished" = YearPublished,
        "BookIndex" = BookIndex
    WHERE "Id" = Id;
end
$$

-- удаление
CREATE PROCEDURE "sp_DeleteBook"(
	Id UUID
)
LANGUAGE plpgsql
as $$
BEGIN   
    DELETE FROM "Books" WHERE "Id" = Id;
end
$$

-- получение данных
CREATE FUNCTION "sp_GetBookById"(BookId UUID)
returns table (
	"Id" UUID,
	"Author" TEXT,
	"Title" TEXT,
	"YearPublished" TIMESTAMPTZ,
	"BookIndex" XML
)
LANGUAGE plpgsql
as $$
BEGIN
	RETURN QUERY
    SELECT b."Id", b."Author", b."Title", b."YearPublished", b."BookIndex"
    FROM "Books" b
    WHERE b."Id" = BookId;
END
$$

-- поиск по библиотеке
CREATE FUNCTION "sp_SearchBooks"(p_Title TEXT, p_Author TEXT, p_Index TEXT)
returns table (
	"Id" UUID,
	"Author" TEXT,
	"Title" TEXT,
	"YearPublished" TIMESTAMPTZ,
	"BookIndex" XML
)
LANGUAGE plpgsql
as $$
BEGIN
	RETURN QUERY
    SELECT b."Id", b."Author", b."Title", b."YearPublished", b."BookIndex"
    FROM "Books" b
    WHERE 
	(p_Author IS NULL OR b."Author" ~* p_Author) AND 
	(p_Title IS NULL OR b."Title" ~* p_Title) AND
	(b."BookIndex"::TEXT ILIKE CONCAT('%', p_Index, '%'));
END
$$