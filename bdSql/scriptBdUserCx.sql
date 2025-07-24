CREATE DATABASE  IF NOT EXISTS `users_test_cx` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `users_test_cx`;
-- MySQL dump 10.13  Distrib 8.0.42, for Win64 (x86_64)
--
-- Host: localhost    Database: users_test_cx
-- ------------------------------------------------------
-- Server version	8.0.41

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
-- Table structure for table `users_cx`
--

DROP TABLE IF EXISTS `users_cx`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users_cx` (
  `id_user` int NOT NULL AUTO_INCREMENT,
  `name_user` varchar(80) NOT NULL,
  `email_user` varchar(50) NOT NULL,
  `age_user` int DEFAULT NULL,
  `gender_user` char(1) DEFAULT NULL,
  `password_user` varchar(200) NOT NULL,
  `picture_user` mediumblob,
  `official_number_user` varchar(14) NOT NULL,
  `search_field` varchar(50) NOT NULL,
  PRIMARY KEY (`id_user`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users_cx`
--

LOCK TABLES `users_cx` WRITE;
/*!40000 ALTER TABLE `users_cx` DISABLE KEYS */;
INSERT INTO `users_cx` VALUES (1,'Leonardo Silverio Lopes','leo_sil117@hotmail.com',27,'M','AQAAAAIAAYagAAAAEJtx5N3zRXuYXBSLwedVCL2zzhNWr6NzMkb7D9qG3akvUhFGGaiHEvxp2wFKJhLphQ==',NULL,'480.436.548-67','LEONARDOSILVERIOLOPES'),(2,'Felipe Mário Kronemberger','felipe.0785@gmail.com',40,'M','AQAAAAIAAYagAAAAEOphwfzBPP4IzWfTwVlvNtQCZT2as+uP0BsHOgrB8n5wC5Mp8d/6R8KkPfsmQbn16w==',NULL,'932.777.098-68','FELIPEMÁRIOKRONEMBERGER'),(3,'Patricia Carriel Silverio','paty181@hotmail.com',53,'F','AQAAAAIAAYagAAAAEID0E79fvz+0P5wfHeUDdHziXEHCk94mFV3wfg1J8jfgkw+xcaPcWMHPYvX5zjyXcg==',NULL,'932.777.098-68','FELIPEMARIOKRONEMBERGER'),(4,'Sueli Maria Carriel Silverio','sueli.carriel2@gmail.com',72,'F','AQAAAAIAAYagAAAAEJRbnM+uGJAPIKFY1bfMxuaS4voQty0QfZwl01LFhu+TKeos80rEm0L6uUYWolCfWQ==',NULL,'932.777.098-68','SUELIMARIACARRIELSILVERIO'),(5,'Alexandre Gomes de Oliveira','alexandre.twex@hotmail.com',34,'M','AQAAAAIAAYagAAAAEAXZatu+85UrXZjSjV3YziXhAbcGEqzgA3Zef1+wcb3twb3L15YQSLHmGnjvd3DciQ==',NULL,'932.777.098-68','ALEXANDREGOMESDEOLIVEIRA'),(6,'Dannylo Carlos','dann.carlos@outlook.com',27,'M','AQAAAAIAAYagAAAAEPULNGNeYNimZ7ppsQBV/xGA3gHWgwoxl+5whY6bJnxUIvZnzZv0MHuuWnbmjQ6C2g==',NULL,'932.777.098-68','DANNYLOCARLOS'),(7,'Ivan Rodrigues','ivan.rodrigues@gmail.com',31,'M','AQAAAAIAAYagAAAAEPULNGNeYNimZ7ppsQBV/xGA3gHWgwoxl+5whY6bJnxUIvZnzZv0MHuuWnbmjQ6C2g==',NULL,'932.777.098-68','IVANRODRIGUES'),(8,'Paula Fernanda Carriel Silverio Soares','paulafernanda.cssoares@gmail.com',41,'F','AQAAAAIAAYagAAAAELN2lS7FTKj8U4tuOPATk1FzLUUr/r1hLTm/P02X/2ovH0Zmrs+H07MDKRlKCbS5Lg==',NULL,'932.777.098-68','PAULAFERNANDACARRIELSILVERIOSOARES'),(10,'Luis Claudio Ferreira Soares','luis.soares@yahoo.com.br',45,'M','AQAAAAIAAYagAAAAED1pWmza/TOU6W9RlAMunAHKSHvb7KWOF+Rg/D75ZbY8un/2cA1r06X8liRR5+L53Q==',NULL,'932.777.098-68','LUISCLAUDIOFERREIRASOARES'),(11,'João Vitor da Silva Ribeiro','jvictor_2711@hotmail.com',29,'F','AQAAAAIAAYagAAAAEBs95dMKM/tQbLQsmcawrfmcvVI0Op7gU2hguyWqm35wynw17asNzoOFdJz2CpA3Wg==',NULL,'475.777.598-99','JOAOVITORDASILVARIBEIRO'),(12,'Edivania Gilda da Silva Lima','edivania_lima@hotmail.com',43,'F','AQAAAAIAAYagAAAAEFudrRx57h95ahl4WgunmcTUUEKwJLw9csW87BSecfUTDxbKQte19WdnwE0fndB+GA==',NULL,'438.137.758-36','EDIVANIAGILDADASILVALIMA'),(13,'Frederico Grubber Ferreira','fred.ferreira@gmail.com',43,'M','AQAAAAIAAYagAAAAECPrmvgMdhHPqdGjkcZgwhbx9Ogdtgqso+ZlVQtI/f7KDHFVFwwpNCFfgpOLoQMLZg==',NULL,'480.436.548-67','FREDERICOGRUBBERFERREIRA');
/*!40000 ALTER TABLE `users_cx` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'users_test_cx'
--

--
-- Dumping routines for database 'users_test_cx'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-07-23 22:56:21
