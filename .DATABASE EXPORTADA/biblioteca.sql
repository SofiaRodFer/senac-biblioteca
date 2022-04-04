-- MySQL dump 10.13  Distrib 8.0.26, for Win64 (x86_64)
--
-- Host: localhost    Database: biblioteca
-- ------------------------------------------------------
-- Server version	8.0.26

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20220330235756_CreateDatabase','3.0.0');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `emprestimos`
--

DROP TABLE IF EXISTS `emprestimos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `emprestimos` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `DataEmprestimo` datetime(6) NOT NULL,
  `DataDevolucao` datetime(6) NOT NULL,
  `NomeUsuario` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Telefone` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Devolvido` tinyint(1) NOT NULL,
  `LivroId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Emprestimos_LivroId` (`LivroId`),
  CONSTRAINT `FK_Emprestimos_Livros_LivroId` FOREIGN KEY (`LivroId`) REFERENCES `livros` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `emprestimos`
--

LOCK TABLES `emprestimos` WRITE;
/*!40000 ALTER TABLE `emprestimos` DISABLE KEYS */;
INSERT INTO `emprestimos` VALUES (1,'2022-03-30 00:00:00.000000','2022-04-06 00:00:00.000000','Sofia','48999999999',0,7),(2,'2022-04-09 00:00:00.000000','2022-04-13 00:00:00.000000','Fábio','48999999999',0,4),(3,'2022-03-30 00:00:00.000000','2022-03-29 00:00:00.000000','Sofia','48999999999',0,1),(4,'2022-03-23 00:00:00.000000','2022-03-02 00:00:00.000000','Teste','48999999999',0,4),(5,'0001-01-01 00:00:00.000000','0001-01-01 00:00:00.000000',NULL,'345',0,1),(6,'2022-03-24 00:00:00.000000','2022-03-29 00:00:00.000000','Carlos','48999999999',0,3),(7,'2022-03-24 00:00:00.000000','2022-04-07 00:00:00.000000','Teste','1',0,6),(8,'2022-03-03 00:00:00.000000','2022-04-08 00:00:00.000000','Teste','6',0,5),(9,'2022-03-19 00:00:00.000000','2022-03-23 00:00:00.000000','Teste','a',0,2),(10,'2022-03-05 00:00:00.000000','2022-04-02 00:00:00.000000','AA','e',0,5),(11,'2022-03-12 00:00:00.000000','2022-04-06 00:00:00.000000','s','r',0,5);
/*!40000 ALTER TABLE `emprestimos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `livros`
--

DROP TABLE IF EXISTS `livros`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `livros` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Titulo` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Autor` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Ano` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `livros`
--

LOCK TABLES `livros` WRITE;
/*!40000 ALTER TABLE `livros` DISABLE KEYS */;
INSERT INTO `livros` VALUES (1,'1984','George Orwell',1949),(2,'O Estrangulador','Sidney Sheldon',1994),(3,'Revolução dos Bichos','George Orwell',1945),(4,'A Outra Face','Deborah Ellis',2000),(5,'Histórias Extraordinárias - Vol. 3','Edgar Allan Poe',1845),(6,'Eragon','Christopher Paolini',2002),(7,'O Beijo da Morte','James Patterson',1995),(8,'O Incêndio de Troia','Marion Zimmer Bradley',1987),(9,'Eldest','Christopher Paolini',2005),(10,'Brisingr','Christopher Paolini',2008),(11,'Herança','Christopher Paolini',2011);
/*!40000 ALTER TABLE `livros` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuarios` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Username` longtext,
  `Senha` longtext,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios`
--

LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` VALUES (1,'admin','x2ghLcB/v8k='),(2,'sofia','x2ghLcB/v8k='),(9,'Testes','x2ghLcB/v8k='),(10,'Teste2','nhgoldDCgNs='),(11,'Teste3','DSBa38Sy6kEOJ/VgQ1kMWA=='),(12,'Teste4','uKM59gXU66BQDdl1uVv/dg=='),(13,'Teste5','obJDvyDry5YKX7xTMvH0SpMk67z/3hb1'),(14,'Teste6','UTqCQAoKsSg='),(15,'Teste7','dlBM/t922iE='),(16,'Teste8','g4yXoN0TkJE='),(17,'Teste9','jyNUoJ15EzA=');
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-04-01 14:27:51
