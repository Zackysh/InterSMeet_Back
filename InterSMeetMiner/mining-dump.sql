-- MySQL dump 10.13  Distrib 5.5.62, for Win64 (AMD64)
--
-- Host: localhost    Database: mining
-- ------------------------------------------------------
-- Server version	8.0.27

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `degree`
--

CREATE DATABASE `mining`;
USE `mining`;

DROP TABLE IF EXISTS `degree`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `degree` (
  `degree_id` int NOT NULL,
  `name` varchar(100) NOT NULL,
  `family_id` int NOT NULL,
  `level_id` int NOT NULL,
  PRIMARY KEY (`degree_id`),
  UNIQUE KEY `degree_un` (`name`,`family_id`,`level_id`),
  KEY `degree_FK` (`level_id`),
  KEY `degree_FK_1` (`family_id`),
  CONSTRAINT `degree_FK` FOREIGN KEY (`level_id`) REFERENCES `level` (`level_id`),
  CONSTRAINT `degree_FK_1` FOREIGN KEY (`family_id`) REFERENCES `family` (`family_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `degree`
--

LOCK TABLES `degree` WRITE;
/*!40000 ALTER TABLE `degree` DISABLE KEYS */;
INSERT INTO `degree` VALUES (1,'Acceso y Conservación en Instalaciones Deportivas',1,1),(63,'Aceites de Oliva y Vinos',11,2),(97,'Acondicionamiento Físico',1,3),(4,'Actividades Agropecuarias',3,1),(46,'Actividades Comerciales',5,2),(16,'Actividades de Panadería y Pastelería',9,1),(20,'Actividades de Panadería y Pastelería',11,1),(29,'Actividades Domésticas y Limpieza de Edificios',16,1),(35,'Actividades Ecuestres',1,2),(38,'Actividades Ecuestres',3,2),(27,'Actividades Marítimo-Pesqueras',15,1),(151,'Acuicultura',15,3),(144,'Administración de Sistemas Informáticos en Red',12,3),(99,'Administración y Finanzas',2,3),(127,'Agencias de Viajes y Gestión de Eventos',9,3),(5,'Agro-jardinería y Composiciones Florales',3,1),(17,'Alojamiento y Lavandería',9,1),(157,'Anatomía Patológica y Citodiagnóstico',24,3),(169,'Animación Sociocultural y Turística',16,3),(137,'Animaciones 3D, Juegos y Entornos Interactivos',21,3),(39,'Aprovechamiento y Conservación del Medio Natural',3,2),(6,'Aprovechamientos Forestales',3,1),(30,'Arreglo y Reparación de Artículos Textiles y de Piel',17,1),(7,'Artes Gráficas',4,1),(132,'Asesoría de Imagen Personal y Corporativa',10,3),(100,'Asistencia a la Dirección',2,3),(84,'Atención a Personas en Situación de Dependencia',16,2),(191,'Audiodescripción y Subtitulación',21,4),(158,'Audiología Protésica',24,3),(114,'Automatización y Robótica Industrial',7,3),(179,'Automoción',18,3),(85,'Calzado y Complementos de Moda',17,2),(133,'Caracterización y Maquillaje Profesional',10,3),(26,'Carpintería y Mueble',14,1),(72,'Carpintería y Mueble',14,2),(88,'Carrocería',18,2),(119,'Centrales Eléctricas',20,3),(193,'Ciberseguridad en Entornos de las Tecnologías de la Información',12,4),(186,'Ciberseguridad en Entornos de las Tecnologías de Operación',7,4),(57,'Cocina y Gastronomía',9,2),(18,'Cocina y Restauración',9,1),(47,'Comercialización de Productos Alimentarios',5,2),(58,'Comercialización de Productos Alimentarios',9,2),(107,'Comercio Internacional',5,3),(89,'Conducción de Vehículos de Transporte por Carretera',18,2),(86,'Confección y Moda',17,2),(53,'Conformado por Moldeo de Metales y Polímeros',8,2),(48,'Construcción',6,2),(123,'Construcciones Metálicas',8,3),(166,'Coordinación de Emergencias y Protección Civil',25,3),(75,'Cultivos Acuícolas',15,2),(198,'Cultivos Celulares',23,4),(145,'Desarrollo de Aplicaciones Multiplataforma',12,3),(146,'Desarrollo de Aplicaciones Web',12,3),(147,'Desarrollo de Proyectos de Instalaciones Térmicas y de Fluidos',13,3),(194,'Desarrollo de videojuegos y realidad virtual',12,4),(185,'Desarrollo y Fabricación de Productos Cerámicos',19,3),(195,'Digitalización del Mantenimiento Industrial',13,4),(128,'Dirección de Cocina',9,3),(129,'Dirección de Servicios de Restauración',9,3),(124,'Diseño en Fabricación Mecánica',8,3),(175,'Diseño Textil y Piel',17,3),(150,'Diseño y Amueblamiento',14,3),(104,'Diseño y Edición de Publicaciones Impresas y Multimedia',4,3),(105,'Diseño y Gestión de la Producción Gráfica',4,3),(176,'Diseño y Producción de Calzado y Complementos',17,3),(159,'Documentación y Administración Sanitarias',24,3),(170,'Educación Infantil',16,3),(167,'Educación y Control Ambiental',25,3),(120,'Eficiencia Energética y Energía Solar Térmica',20,3),(64,'Elaboración de Productos Alimenticios',11,2),(10,'Electricidad y Electrónica',7,1),(90,'Electromecánica de Maquinaria',18,2),(91,'Electromecánica de Vehículos Automóviles',18,2),(115,'Electromedicina Clínica',7,3),(81,'Emergencias Sanitarias',24,2),(83,'Emergencias y Protección Civil',25,2),(121,'Energías Renovables',20,3),(98,'Enseñanza y Animación Sociodeportiva',1,3),(135,'Estética Integral y Bienestar',10,3),(60,'Estética y Belleza',10,2),(134,'Estilismo y Dirección de Peluquería',10,3),(66,'Excavaciones y Sondeos',22,2),(189,'Fabricación Aditiva',8,4),(11,'Fabricación de Elementos Metálicos',7,1),(13,'Fabricación de Elementos Metálicos',8,1),(96,'Fabricación de Productos Cerámicos',19,2),(154,'Fabricación de Productos Farmacéuticos, Biotecnológicos y Afines',23,3),(196,'Fabricación Inteligente',13,4),(87,'Fabricación y Ennoblecimiento de Productos Textiles',17,2),(14,'Fabricación y Montaje',8,1),(24,'Fabricación y Montaje',13,1),(82,'Farmacia y Parafarmacia',24,2),(171,'Formación para la Movilidad Segura y Sostenible',16,3),(101,'Ganadería y Asistencia en Sanidad Animal',3,3),(37,'Gestión Administrativa',2,2),(130,'Gestión de Alojamientos Turísticos',9,3),(108,'Gestión de Ventas y Espacios Comerciales',5,3),(122,'Gestión del Agua',20,3),(102,'Gestión Forestal y del Medio Natural',3,3),(36,'Guía en el Medio Natural y de Tiempo Libre',1,2),(131,'Guía, Información y Asistencias Turísticas',9,3),(160,'Higiene Bucodental',24,3),(138,'Iluminación, Captación y Tratamiento de Imagen',21,3),(161,'Imagen para el Diagnóstico y Medicina Nuclear',24,3),(187,'Implementación de redes 5G',7,4),(43,'Impresión Gráfica',4,2),(21,'Industrias Alimentarias',11,1),(2,'Informática de Oficina',2,1),(22,'Informática de Oficina',12,1),(23,'Informática y Comunicaciones',12,1),(73,'Instalación y Amueblamiento',14,2),(70,'Instalaciones de Producción de Calor',13,2),(51,'Instalaciones de Telecomunicaciones',7,2),(50,'Instalaciones Eléctricas y Automáticas',7,2),(12,'Instalaciones Electrotécnicas y Mecánica',7,1),(15,'Instalaciones Electrotécnicas y Mecánica',8,1),(69,'Instalaciones Frigoríficas y de Climatización',13,2),(172,'Integración Social',16,3),(192,'Inteligencia Artificial y Big Data',12,4),(40,'Jardinería y Floristería',3,2),(162,'Laboratorio Clínico y Biomédico',24,3),(155,'Laboratorio de Análisis y de Control de Calidad',23,3),(180,'Mantenimiento Aeromecánico de Aviones con Motor de Pistón',18,3),(181,'Mantenimiento Aeromecánico de Aviones con Motor de Turbina',18,3),(182,'Mantenimiento Aeromecánico de Helicópteros con Motor de Pistón',18,3),(183,'Mantenimiento Aeromecánico de Helicópteros con Motor de Turbina',18,3),(200,'Mantenimiento avanzado de material rodante ferroviario',18,4),(92,'Mantenimiento de Embarcaciones de Recreo',18,2),(28,'Mantenimiento de Embarcaciones Deportivas y de Recreo',15,1),(32,'Mantenimiento de Embarcaciones Deportivas y de Recreo',18,1),(93,'Mantenimiento de Estructuras de Madera y Mobiliario de Embarcaciones de Recreo',18,2),(148,'Mantenimiento de Instalaciones Térmicas y de Fluidos',13,3),(94,'Mantenimiento de Material Rodante Ferroviario',18,2),(184,'Mantenimiento de Sistemas Electrónicos y Aviónicos de Aeronaves',18,3),(33,'Mantenimiento de Vehículos',18,1),(199,'Mantenimiento de Vehículos Híbridos y Eléctricos',18,4),(25,'Mantenimiento de Viviendas',13,1),(71,'Mantenimiento Electromecánico',13,2),(116,'Mantenimiento Electrónico',7,3),(76,'Mantenimiento y Control de la Maquinaria de Buques y Embarcaciones',15,2),(109,'Marketing y Publicidad',5,3),(54,'Mecanizado',8,2),(149,'Mecatrónica Industrial',13,3),(173,'Mediación Comunicativa',16,3),(197,'Modelado de la información en la construcción (BIM)',13,4),(55,'Montaje de Estructuras e Instalación de Sistemas Aeronáuticos',8,2),(95,'Montaje de Estructuras e Instalación de Sistemas Aeronáuticos',18,2),(77,'Navegación y Pesca de Litoral',15,2),(49,'Obras de Interior, Decoración y Rehabilitación',6,2),(79,'Operaciones de Laboratorio',23,2),(78,'Operaciones Subacuáticas e Hiperbáricas',15,2),(152,'Organización del Mantenimiento de Maquinaria de Buques y Embarcaciones',15,3),(111,'Organización y Control de Obras de Construcción',6,3),(163,'Ortoprótesis y Productos de Apoyo',24,3),(103,'Paisajismo y Medio Rural',3,3),(190,'Panadería y Bollería Artesanales',9,4),(65,'Panadería, Repostería y Confitería',11,2),(177,'Patronaje y Moda',17,3),(61,'Peluquería y Cosmética Capilar',10,2),(19,'Peluquería y Estética',10,1),(67,'Piedra Natural',22,2),(80,'Planta Química',23,2),(44,'Postimpresión y Acabados Gráficos',4,2),(45,'Preimpresión Digital',4,2),(74,'Procesado y Transformación de la Madera',14,2),(142,'Procesos y Calidad en la Industria Alimentaria',11,3),(41,'Producción Agroecológica',3,2),(42,'Producción Agropecuaria',3,2),(139,'Producción de Audiovisuales y Espectáculos',21,3),(125,'Programación de la Producción en Fabricación Mecánica',8,3),(126,'Programación de la Producción en Moldeo de Metales y Polímeros',8,3),(174,'Promoción de Igualdad de Género',16,3),(164,'Prótesis Dentales',24,3),(112,'Proyectos de Edificación',6,3),(113,'Proyectos de Obra Civil',6,3),(156,'Química Industrial',23,3),(168,'Química y Salud Ambiental',25,3),(165,'Radioterapia y Dosimetría',24,3),(140,'Realización de Proyectos Audiovisuales y Espectáculos',21,3),(52,'Redes y Estaciones de Tratamiento de Aguas',20,2),(9,'Reforma y Mantenimiento de Edificios',6,1),(3,'Servicios Administrativos',2,1),(8,'Servicios Comerciales',5,1),(59,'Servicios en Restauración',9,2),(188,'Sistemas de señalización y telecomunicaciones ferroviarias',7,4),(118,'Sistemas de Telecomunicaciones e Informáticos',7,3),(117,'Sistemas Electrotécnicos y Automatizados',7,3),(68,'Sistemas Microinformáticos y Redes',12,2),(56,'Soldadura y Calderería',8,2),(141,'Sonido para Audiovisuales y Espectáculos',21,3),(31,'Tapicería y Cortinaje',17,1),(106,'Técnico Superior Artista Fallero y Construcción de Escenografías',26,3),(136,'Termalismo y Bienestar',10,3),(153,'Transporte Marítimo y Pesca de Altura',15,3),(110,'Transporte y Logística',5,3),(178,'Vestuario a Medida y de Espectáculos',17,3),(62,'Vídeo Disc-Jockey y Sonido',21,2),(34,'Vidriería y Alfarería',19,1),(143,'Vitivinicultura',11,3);
/*!40000 ALTER TABLE `degree` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `family`
--

DROP TABLE IF EXISTS `family`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `family` (
  `family_id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`family_id`),
  UNIQUE KEY `family_un` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `family`
--

LOCK TABLES `family` WRITE;
/*!40000 ALTER TABLE `family` DISABLE KEYS */;
INSERT INTO `family` VALUES (1,'Actividades Físicas y Deportivas'),(2,'Administración y Gestión'),(3,'Agraria'),(4,'Artes Gráficas'),(26,'Artes y Artesanias'),(5,'Comercio y Marketing'),(6,'Edificación y Obra Civil'),(7,'Electricidad y Electrónica'),(20,'Energía y Agua'),(8,'Fabricación Mecánica'),(9,'Hostelería y Turismo'),(10,'Imagen Personal'),(21,'Imagen y Sonido'),(11,'Industrias Alimentarias'),(22,'Industrias Extractivas'),(12,'Informática y Comunicaciones'),(13,'Instalación y Mantenimiento'),(14,'Madera, Mueble y Corcho'),(15,'Marítimo-Pesquera'),(23,'Química'),(24,'Sanidad'),(25,'Seguridad y Medio Ambiente'),(16,'Servicios Socioculturales y a la Comunidad'),(17,'Textil, Confección y Piel'),(18,'Transporte y Mantenimiento de Vehículos'),(19,'Vidrio y Cerámica');
/*!40000 ALTER TABLE `family` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `level`
--

DROP TABLE IF EXISTS `level`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `level` (
  `level_id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`level_id`),
  UNIQUE KEY `level_un` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `level`
--

LOCK TABLES `level` WRITE;
/*!40000 ALTER TABLE `level` DISABLE KEYS */;
INSERT INTO `level` VALUES (1,'Ciclo_Formativo_Basico'),(4,'Ciclo_Formativo_Especifico'),(2,'Ciclo_Formativo_Medio'),(3,'Ciclo_Formativo_Superior');
/*!40000 ALTER TABLE `level` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'mining'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-11-24 23:05:05
