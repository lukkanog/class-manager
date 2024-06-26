IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'aluno')
BEGIN
    CREATE TABLE aluno (
        id INT IDENTITY PRIMARY KEY,
        nome VARCHAR(255),
        usuario VARCHAR(45) UNIQUE,
        senha CHAR(60)
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'turma')
BEGIN
    CREATE TABLE turma (
        id INT IDENTITY PRIMARY KEY,
        curso_id INT,
        turma VARCHAR(45) UNIQUE,
        ano INT
    );
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'aluno_turma')
BEGIN
    CREATE TABLE aluno_turma (
        aluno_id INT FOREIGN KEY REFERENCES aluno(id) ON DELETE CASCADE,
        turma_id INT FOREIGN KEY REFERENCES turma(id) ON DELETE CASCADE,

        PRIMARY KEY (aluno_id, turma_id)
    );
END
GO
