using System;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;
public class Programa
{
    public static void Main()
    {
        string pathAdmin = "Admin.txt";
        string pathUsuarios = "Usuarios.txt";
        string pathLibros = "Libros.txt";
        string pathRecomendaciones = "Recomendaciones.txt";
        string pathPrestamos = "Prestamos.txt";
        string pathBit = "Bitacora.txt";
        VerificaDoc(pathAdmin);
        VerificaDoc(pathUsuarios);
        VerificaDoc(pathLibros);
        VerificaDoc(pathRecomendaciones);
        VerificaDoc(pathPrestamos);
        VerificaDoc(pathBit);
        EjecucionJefe(pathAdmin, pathUsuarios, pathLibros, pathRecomendaciones, pathPrestamos, pathBit);
    }
    public static void MenuJefe()
    {
        Console.WriteLine("Bienvenido a LoveBooks");
        Console.WriteLine("1-Ya eres Usario");
        Console.WriteLine("2-Registrate");
        Console.WriteLine("3-Administrador");
        Console.WriteLine("4-Salir");
    }
    public static void MostrarMenu()
    {
        Console.WriteLine("LoveBooks");
        Console.WriteLine("1-Administración de Usuarios");
        Console.WriteLine("2-Administración de Libros");
        Console.WriteLine("3-Buscar Libro");
        Console.WriteLine("4-Prestamos");
        Console.WriteLine("5-Ver Bitacora");
        Console.WriteLine("6-Cerrar secion");
        Console.WriteLine("7-Salir");
    }
    public static void MostrarMenuUser()
    {
        Console.WriteLine("LoveBooks");
        Console.WriteLine("1-Buscar Libro");
        Console.WriteLine("2-Prestamos");
        Console.WriteLine("3-Cerrar secion");
        Console.WriteLine("4-Salir");
    }
    public static void MostrarMenuLibro()
    {
        Console.WriteLine("1-Buscar libro por ID");
        Console.WriteLine("2-Buscar libro por Autor");
        Console.WriteLine("3-Buscar libro por el Nombre del Libro");
        Console.WriteLine("4-Buscar libro por la Categoria");
        Console.WriteLine("5-Buscar libro por la Editorial");
        Console.WriteLine("6-Buscar libro por el Idioma");
        Console.WriteLine("7-Dejamos tus recomendaciones:");
        Console.WriteLine("8-Regresar");
        Console.WriteLine("9-Cerrar secion");
        Console.WriteLine("10-Salir");
    }
    public static void MostrarMenuPrestamos()
    {
        Console.WriteLine("1-Solicitar Prestamo");
        Console.WriteLine("2-Devolver Libro");
        Console.WriteLine("3-Transferir Prestamo");
        Console.WriteLine("4-Verificar Multas y Dias Restantes");
        Console.WriteLine("5-Regresar");
        Console.WriteLine("6-Cerrar secion");
        Console.WriteLine("7-Salir");
    }
    public static void LogIn(string pathAdmin, string pathUsuarios, string pathLibros, string pathPrestamos,string pathRecomendaciones, string pathBit)
    {
        Console.Clear();
        string[,] datosAdmin = CrearMatrizAdmin(pathAdmin);
        string[,] datos = CrearMatrizUsuario(pathUsuarios);
        string[,] datosLib = CrearMatrizLibro(pathLibros);
        string[,] datosRec = CrearMatrizRec(pathRecomendaciones);
        string[,] datosPres = CrearMatrizPres(pathPrestamos);
        string[,] datosBit = CrearMatrizBit(pathBit);
        string id = EntradaDatos("Ingresa Matricula");
        Console.Clear();
        string psw = EntradaDatos("Ingresa Contraseña");
        Console.Clear();
        int userFlow = EncontrarCasillaMatriz(datosAdmin, id);
        if (userFlow >= 0)
        {
            if (datosAdmin[userFlow, 1].Trim().Equals(psw))
            {
                Console.WriteLine("Contraseña correcta");
                Console.Clear();
                Ejecucion(pathAdmin, pathUsuarios, pathLibros, pathRecomendaciones, pathPrestamos, pathBit, datosBit, datos,
                    datosLib, datosRec, datosPres);
            }
            else Console.WriteLine("Credenciales Incorrectas");
        }
        else
        {
            EjecucionJefe(pathAdmin, pathUsuarios, pathLibros, pathRecomendaciones, pathPrestamos, pathBit);
        }
    }
    public static void LogInUser(string pathAdmin,string pathUsuarios, string pathLibros, string pathPrestamos, string pathRecomendaciones,string pathBit)
    {
        Console.Clear();
        string[,] datos = CrearMatrizUsuario(pathUsuarios);
        string[,] datosLib = CrearMatrizLibro(pathLibros);
        string[,] datosRec = CrearMatrizRec(pathRecomendaciones);
        string[,] datosPres = CrearMatrizPres(pathPrestamos);
        string[,] datosBit = CrearMatrizBit(pathBit);
        string id = EntradaDatos("Ingresa Expediente");
        Console.Clear();
        string psw = EntradaDatos("Ingresa Contraseña");
        Console.Clear();
        int userFlow = EncontrarCasillaMatriz(datos, id);
        if (userFlow >= 0)
        {
            if (datos[userFlow, 3].Trim().Equals(psw))
            {
                Console.WriteLine("Contraseña correcta");
                Console.Clear();
                EjecucionUser(pathAdmin,pathUsuarios,pathLibros,pathRecomendaciones,pathPrestamos,pathBit,datos,datosLib,datosRec,datosPres,datosBit);
            }
            else Console.WriteLine("Credenciales Incorrectas");
        }
        else
        {
            //Crea o dar alta del usuario
            Console.WriteLine("Uusario Inexistente");
            DarAlta(datos);
            EscribirCambiosAlta(pathUsuarios, datos);
        }
    }
    public static void EjecucionJefe(string pathAdmin, string pathUsuarios, string pathLibros,string pathRecomendaciones, string pathPrestamos, string pathBit)
    {
        MenuJefe();
        string entrada = EntradaDatos("Ingresa una opcion");
        switch (entrada)
        {
            case "1"://Ya hay usuario
                LogInUser(pathUsuarios, pathLibros, pathPrestamos, pathRecomendaciones, pathBit, pathAdmin);
                break;
            case "2"://Registro
                string[,] datos = CrearMatrizUsuario(pathUsuarios);
                DarAlta(datos);
                EscribirCambiosAlta(pathUsuarios, datos);
                break;
            case "3"://admin
                LogIn(pathAdmin, pathUsuarios, pathLibros, pathPrestamos, pathRecomendaciones,
                    pathBit);
                break;
            case "4"://salir
                break;
        }
    }
    public static void Ejecucion(string pathAdmin,string pathUsuarios, string pathLibros, string pathRecomendaciones,string pathPrestamos, string pathBit, string[,] datosBit, string[,] datos, string[,] datosLib,string[,] datosRec, string[,] datosPres)
    {
        Console.Clear();
        MostrarMenu();
        string entrada = EntradaDatos("Ingresa tu opcion: ");
        switch (entrada)
        {
            case "1":
                AdministacionUsuarios(pathUsuarios, datos, datosPres);
                break;
            case "2":
                AdministacionLibros(pathLibros, datosLib);
                break;
            case "3"://buscar libro
                EjecucionLibro(pathAdmin,pathLibros, pathRecomendaciones, pathUsuarios, pathPrestamos, datos,
                    datosLib, datosRec, datosPres, pathBit, datosBit);
                break;
            case "4": //Prestamo
                EjecucionPres(pathAdmin, pathBit,pathUsuarios, pathLibros,pathRecomendaciones, pathPrestamos,datos,datosLib, datosRec, datosPres, datosBit);
                break;
            case "5"://Ver bitacora
                Console.Clear();
                ImprimirMatriz(datosBit);
                break;
            case "6"://cerrar cesion 
                EjecucionJefe(pathAdmin, pathUsuarios, pathLibros, pathRecomendaciones, pathPrestamos, pathBit);
                break;
            case "7"://salir
                break;
        }
    }
    public static void EjecucionUser(string pathAdmin, string pathUsuarios, string pathLibros, string pathRecomendaciones,string pathPrestamos,string pathBit, string[,] datos, string[,] datosLib, string[,] datosRec, string[,] datosPres, string[,]datosBit)
    {
        Console.Clear();
        MostrarMenuUser();
        string entrada = EntradaDatos("Ingresa tu opcion: ");
        switch (entrada)
        {
            case "1"://buscar libro
                EjecucionLibro2(pathAdmin, pathLibros, pathRecomendaciones, pathUsuarios, pathPrestamos,pathBit, datos, datosLib, datosRec, datosPres, datosBit);
                break;
            case "2": //Solicitud Prestamo
                EjecucionPres2(pathAdmin, pathBit, pathUsuarios, pathLibros, pathRecomendaciones, pathPrestamos, datos, datosLib, datosRec, datosPres,datosBit);
                break;
            case "3"://cerrar cesion
                EjecucionJefe(pathAdmin, pathUsuarios, pathLibros, pathRecomendaciones, pathPrestamos, pathBit);
                break;
            case "4"://salir
                break;
        }
    }
    public static void EjecucionLibro(string pathAdmin,string pathLibros, string pathRecomendaciones, string pathUsuarios,string pathPrestamos, string[,] datos, string[,] datosLib, string[,] datosRec, string[,] datosPres, string pathBit, string[,] datosBit)
    {
        Console.Clear();
        MostrarMenuLibro();
        string lib = EntradaDatos("Ingresa una opcion:");
        string filtra;
        switch (lib)
        {
            case "1"://id
                Console.Clear();
                filtra = EntradaDatos("Cual quieres buscar");
                Filtrado(datosLib, filtra, 0);
                Console.WriteLine("¿Quieres solicitar el prestamo, si tu respuesta es si presiona 1?");
                int Rect = Convert.ToInt32(Console.ReadLine());
                if (Rect == 1)
                {
                    SolicitarPrestamo(pathUsuarios, pathLibros, pathPrestamos, datos, datosLib, datosPres);
                }
                break;
            case "2"://autor
                Console.Clear();
                filtra = EntradaDatos("Cual Author buscar");
                Filtrado(datosLib, filtra, 1);
                Console.WriteLine("¿Quieres solicitar el prestamo, si tu respuesta es si presiona 1?");
                Rect = Convert.ToInt32(Console.ReadLine());
                if (Rect == 1)
                {
                    SolicitarPrestamo(pathUsuarios, pathLibros, pathPrestamos, datos, datosLib, datosPres);
                }
                break;
            case "3"://libro
                Console.Clear();
                filtra = EntradaDatos("Cual Titulo buscas");
                Filtrado(datosLib, filtra, 2);
                Console.WriteLine("¿Quieres solicitar el prestamo, si tu respuesta es si presiona 1?");
                Rect = Convert.ToInt32(Console.ReadLine());
                if (Rect == 1)
                {
                    SolicitarPrestamo(pathUsuarios, pathLibros, pathPrestamos, datos, datosLib, datosPres);
                }
                break;
            case "4"://categoria
                Console.Clear();
                filtra = EntradaDatos("Cual Genero buscar");
                Filtrado(datosLib, filtra, 3);
                Console.WriteLine("¿Quieres solicitar el prestamo, si tu respuesta es si presiona 1?");
                Rect = Convert.ToInt32(Console.ReadLine());
                if (Rect == 1)
                {
                    SolicitarPrestamo(pathUsuarios, pathLibros, pathPrestamos, datos, datosLib, datosPres);
                }
                break;
            case "5"://editorial
                Console.Clear();
                filtra = EntradaDatos("Cual Editorial buscar");
                Filtrado(datosLib, filtra, 4);
                Console.WriteLine("¿Quieres solicitar el prestamo, si tu respuesta es si presiona 1?");
                Rect = Convert.ToInt32(Console.ReadLine());
                if (Rect == 1)
                {
                    SolicitarPrestamo(pathUsuarios, pathLibros, pathPrestamos, datos, datosLib, datosPres);
                }
                break;
            case "6"://idioma
                Console.Clear();
                filtra = EntradaDatos("En cual idioma buscas");
                Filtrado(datosLib, filtra, 5);
                Console.WriteLine("¿Quieres solicitar el prestamo, si tu respuesta es si presiona 1?");
                Rect = Convert.ToInt32(Console.ReadLine());
                if (Rect == 1)
                {
                    SolicitarPrestamo(pathUsuarios, pathLibros, pathPrestamos, datos, datosLib, datosPres);
                }
                break;
            case "7":
                Console.Clear();
                DarAltaRecomendaciones(datosLib, datosRec);
                break;
            case "8":
                Ejecucion(pathAdmin, pathUsuarios, pathLibros, pathRecomendaciones, pathPrestamos, pathBit, datosBit, datos, datosLib, datosRec, datosPres);
                break;
            case "9":
                EjecucionJefe(pathAdmin, pathUsuarios, pathLibros, pathRecomendaciones, pathPrestamos, pathBit);
                break;
            case "10"://salir
                break;
        }
    }
    public static void EjecucionLibro2(string pathAdmin,string pathLibros, string pathRecomendaciones, string pathUsuarios,string pathPrestamos,string pathBit, string[,] datos, string[,] datosLib, string[,] datosRec, string[,] datosPres, string[,]datosBit)
    {
        Console.Clear();
        MostrarMenuLibro();
        string lib = EntradaDatos("Ingresa una opcion:");
        string filtra;
        switch (lib)
        {
            case "1"://id
                Console.Clear();
                filtra = EntradaDatos("Cual quieres buscar");
                Filtrado(datosLib, filtra, 0);
                Console.WriteLine("¿Quieres solicitar el prestamo, si tu respuesta es si presiona 1?");
                int Rect = Convert.ToInt32(Console.ReadLine());
                if (Rect == 1)
                {
                    SolicitarPrestamo(pathUsuarios, pathLibros, pathPrestamos, datos, datosLib, datosPres);
                }
                break;
            case "2"://autor
                Console.Clear();
                filtra = EntradaDatos("Cual Author buscar");
                Filtrado(datosLib, filtra, 1);
                Console.WriteLine("¿Quieres solicitar el prestamo, si tu respuesta es si presiona 1?");
                Rect = Convert.ToInt32(Console.ReadLine());
                if (Rect == 1)
                {
                    SolicitarPrestamo(pathUsuarios, pathLibros, pathPrestamos, datos, datosLib, datosPres);
                }
                break;
            case "3"://libro
                Console.Clear();
                filtra = EntradaDatos("Cual Titulo buscas");
                Filtrado(datosLib, filtra, 2);
                Console.WriteLine("¿Quieres solicitar el prestamo, si tu respuesta es si presiona 1?");
                Rect = Convert.ToInt32(Console.ReadLine());
                if (Rect == 1)
                {
                    SolicitarPrestamo(pathUsuarios, pathLibros, pathPrestamos, datos, datosLib, datosPres);
                }
                break;
            case "4"://categoria
                Console.Clear();
                filtra = EntradaDatos("Cual Genero buscar");
                Filtrado(datosLib, filtra, 3);
                Console.WriteLine("¿Quieres solicitar el prestamo, si tu respuesta es si presiona 1?");
                Rect = Convert.ToInt32(Console.ReadLine());
                if (Rect == 1)
                {
                    SolicitarPrestamo(pathUsuarios, pathLibros, pathPrestamos, datos, datosLib, datosPres);
                }
                break;
            case "5"://editorial
                Console.Clear();
                filtra = EntradaDatos("Cual Editorial buscar");
                Filtrado(datosLib, filtra, 4);
                Console.WriteLine("¿Quieres solicitar el prestamo, si tu respuesta es si presiona 1?");
                Rect = Convert.ToInt32(Console.ReadLine());
                if (Rect == 1)
                {
                    SolicitarPrestamo(pathUsuarios, pathLibros, pathPrestamos, datos, datosLib, datosPres);
                }
                break;
            case "6"://idioma
                Console.Clear();
                filtra = EntradaDatos("En cual idioma buscas");
                Filtrado(datosLib, filtra, 5);
                Console.WriteLine("¿Quieres solicitar el prestamo, si tu respuesta es si presiona 1?");
                Rect = Convert.ToInt32(Console.ReadLine());
                if (Rect == 1)
                {
                    SolicitarPrestamo(pathUsuarios, pathLibros, pathPrestamos, datos, datosLib, datosPres);
                }
                break;
            case "7":
                Console.Clear();
                DarAltaRecomendaciones(datosLib, datosRec);
                break;
            case "8":
                EjecucionUser(pathAdmin,pathLibros, pathRecomendaciones, pathUsuarios, pathPrestamos, pathBit,datos, datosLib, datosRec, datosPres,datosBit);
                break;
            case "9":
                EjecucionJefe(pathAdmin, pathUsuarios, pathLibros, pathRecomendaciones, pathPrestamos, pathBit);
                break;
            case "10"://salir
                break;
        }
    }
    public static void EjecucionPres(string pathAdmin, string pathBit,string pathUsuarios, string pathLibros, string pathRecomendaciones,string pathPrestamos, string[,] datos, string[,] datosLib, string[,] datosRec, string[,] datosPres, string[,] datosBit)
    {
        Console.Clear();
        MostrarMenuPrestamos();
        string entrada = EntradaDatos("Ingresa una Opcion:");
        switch(entrada)
        {
            case "1"://Solicitar Prestamo
                SolicitarPrestamo(pathUsuarios, pathLibros, pathPrestamos, datos, datosLib, datosPres);
                break;
            case "2"://Devolver Libro
                DevolverLibro(pathUsuarios, pathLibros, pathPrestamos, datosPres, datosLib);
                break;
            case "3"://Traspaso
                TransferirPrestamo(pathUsuarios,pathLibros,pathPrestamos,datos,datosPres,datosBit,pathAdmin,pathBit,pathRecomendaciones,datosLib, datosRec);
                break;
            case "4"://VerificarMultasYDiasRestantes
                VerificarMultasYDiasRestantes(pathPrestamos, datosPres);
                break;
            case "5":
                Ejecucion(pathAdmin, pathUsuarios, pathLibros, pathRecomendaciones, pathPrestamos, pathBit, datosBit, datos, datosLib, datosRec, datosPres);
                break;
            case "6"://cerrar cesion
                EjecucionJefe(pathAdmin, pathUsuarios, pathLibros, pathRecomendaciones, pathPrestamos, pathBit);
                break;//salir
            case "7":
                break;
        }
    }
    public static void EjecucionPres2(string pathAdmin, string pathBit, string pathUsuarios, string pathLibros, string pathRecomendaciones,string pathPrestamos, string[,] datos, string[,] datosLib, string[,] datosRec, string[,] datosPres, string[,]datosBit)
    {
        Console.Clear();
        MostrarMenuPrestamos();
        string entrada = EntradaDatos("Ingresa una Opcion:");
        switch (entrada)
        {
            case "1"://Solicitar Prestamo
                SolicitarPrestamo(pathUsuarios, pathLibros, pathPrestamos, datos, datosLib, datosPres);
                break;
            case "2"://Devolver Libro
                DevolverLibro(pathUsuarios, pathLibros, pathPrestamos, datosPres, datosLib);
                break;
            case "3"://Traspaso
                TransferirPrestamo(pathUsuarios, pathLibros, pathPrestamos, datos, datosPres, datosBit, pathAdmin, pathBit, pathRecomendaciones, datosLib, datosRec);
                break;
            case "4"://VerificarMultasYDiasRestantes
                VerificarMultasYDiasRestantes(pathPrestamos, datosPres);
                break;
            case "5":
                EjecucionUser(pathAdmin, pathLibros, pathRecomendaciones, pathUsuarios, pathPrestamos, pathBit, datos, datosLib, datosRec, datosPres, datosBit);
                break;
            case "6"://cerrar cesion
                EjecucionJefe(pathAdmin, pathUsuarios, pathLibros, pathRecomendaciones, pathPrestamos, pathBit);
                break;//salir
            case "7":
                break;
        }
    }
    public static void EscribirCambios(string path, string[,] arreglo)
    {
        StreamWriter escritura = File.CreateText(path);
        for (int j = 0; j < arreglo.GetLength(0); j++)
        {
            for (int i = 0; i < arreglo.GetLength(1); i++)
            {
                if (i == arreglo.GetLength(1) - 1)
                {
                    escritura.Write(arreglo[j, i]);
                }
                else
                {
                    escritura.Write(arreglo[j, i]);
                    escritura.Write(','); //Va a existir una coma al final
                }
            }
            if (!(j == arreglo.GetLength(0) - 1))
            {
                escritura.WriteLine();
            }

        }
        escritura.Close();
    }
    public static void EscribirCambiosAlta(string path, string[,] arreglo)
    {
        StreamWriter escritura = File.CreateText(path);
        for (int j = 0; j < arreglo.GetLength(0); j++)
        {
            for (int i = 0; i < arreglo.GetLength(1); i++)
            {
                if (i == arreglo.GetLength(1) - 1)
                {
                    escritura.Write(arreglo[j, i]);

                }
                else
                {
                    escritura.Write(arreglo[j, i]);
                    escritura.Write(',');  //Va a existir una coma al final
                }
            }
            escritura.WriteLine();

        }
        escritura.Close();

    }
    public static void AdministacionUsuarios(string path, string[,] datos, string[,]datosPres)
    {
        Console.Clear();
        Console.WriteLine("1-ALta de Usuarios");
        Console.WriteLine("2-Baja de Usuarios");
        Console.WriteLine("3-Cambio de Contraseña");
        Console.WriteLine("4-Salir");
        string entrada= EntradaDatos("Ingresa la Opción deseada:");
        switch(entrada)
        {
            case "1": //Alta
                Console.Clear();
                DarAlta(datos);
                EscribirCambiosAlta(path,datos);
                break;
            case "2": //Baja
                Console.Clear();
                DarBaja(datos,datosPres);
                EscribirCambios(path, datos);
                break;
            case "3"://contraseña
                Console.Clear();
                string exp = EntradaDatos("Ingresa tu expediente: ");
                CambiarDatoMatriz(exp, datos, path, 3);
                break;
            case "4":
                break;
        }
    }
    public static void AdministacionLibros(string path, string[,] datosLib)
    {
        Console.Clear();
        Console.WriteLine("1-ALta de Libros");
        Console.WriteLine("2-Baja de Libros");
        Console.WriteLine("3-Añadir stoks");
        Console.WriteLine("4-Salir");
        string entrada = EntradaDatos("Ingresa la Opción deseada:");
        switch (entrada)
        {
            case "1": //Alta
                Console.Clear();
                DarAltaLibros(datosLib);
                EscribirCambiosAlta(path, datosLib);
                break;
            case "2": //Baja
                Console.Clear();
                DarBajaLibro(datosLib);
                EscribirCambiosAlta(path, datosLib);
                break;
            case "3"://stok
                Console.Clear();
                Stock(path, datosLib);
                break;
            case "4":
                break;
        }
    }
    public static void DarAlta(string[,] datos)
    {
        Console.Clear();
        Console.WriteLine("Alta de Usuario");
        string expediente = EntradaDatos("Igresa tu Expediente");
        if (EncontrarCasillaMatriz(datos, expediente) >= 0)
        {
            Console.WriteLine("Expediente ya existente");
            return;
        }
        string nombre = EntradaDatos("Ingresa tu nombre");
        string carrera = EntradaDatos("Ingresa tu carrera");
        string contraseña = EntradaDatos("Ingresa tu contraseña");
        bool added = false;
        for (int i = 0; i < datos.GetLength(0); i++)
        {
            if (string.IsNullOrEmpty(datos[i, 0]))
            {
                datos[i, 0] = expediente;
                datos[i, 1] = nombre;
                datos[i, 2] = carrera;
                datos[i, 3] = contraseña;
                added = true;//ya registro el usuario
                break;
            }
        }
        if (added)
        {
            EscribirCambiosAlta("Usuarios.txt", datos);
        }
        else { Console.WriteLine("No hay espacios"); }
    }
    public static void DarAltaLibros(string[,] datosLib)
    {
        Console.Clear();
        Console.WriteLine("Alta de Libro");
        string id = EntradaDatos("Igresa el ID");
        if (EncontrarCasillaMatriz(datosLib, id) >= 0)
        {
            Console.WriteLine("ID ya existente");
            return;
        }
        string nA = EntradaDatos("Ingresa el Nombre de Autor");
        string nt = EntradaDatos("Ingresa Titulo del Libro");
        string genero = EntradaDatos("Ingresa el Genero del Libro");
        string editorial = EntradaDatos("Ingresa la editorial");
        string idioma = EntradaDatos("Ingresa el Idioma del Libro");
        string disponible = EntradaDatos("Ingresa el numero de Libros");
        for (int i = 0; i < datosLib.GetLength(0); i++)
        {
            if (string.IsNullOrEmpty(datosLib[i, 0]))
            {
                datosLib[i, 0] = id;
                datosLib[i, 1] = nA;
                datosLib[i, 2] = nt;
                datosLib[i, 3] = genero;
                datosLib[i, 4] = editorial;
                datosLib[i, 5] = idioma;
                datosLib[i, 6] = disponible;
                break;
            }
        }
        EscribirCambiosAlta("Libros.txt", datosLib);
    }
    public static void DarAltaRecomendaciones(string[,] datos, string[,] datosRec)
    {
        Console.Clear();
        Console.WriteLine("Dejanos tus recomendaciones:");
        string autor = EntradaDatos("Ingresa el autor del libro");
        string libro = EntradaDatos("Ingresa el nombre del libro");
        string editorial = EntradaDatos("Ingresa la Editorial del libro");
        string idioma = EntradaDatos("Ingresa el idioma del libro");
        for (int i = 0; i < datosRec.GetLength(0); i++)
        {
            if (string.IsNullOrEmpty(datosRec[i, 0]))
            {
                datosRec[i, 0] = autor;
                datosRec[i, 1] = libro;
                datosRec[i, 2] = editorial;
                datosRec[i, 3] = idioma;
                break;
            }
        }
        EscribirCambiosAlta("Recomendaciones.txt", datosRec);
    }
    public static void DarBaja(string[,] datos, string[,] datosPres)
    {
        string bajaExp = EntradaDatos("Ingresa el expediente a eliminar: ");
        int fila = EncontrarCasillaMatriz(datos, bajaExp);
        if (fila >= 0) // Se encuentra el expediente
        {
            if (MultasActivas(bajaExp, datosPres))
            {
                Console.WriteLine("El usuario tiene multas e lirbos no devueltos y no puede ser dado de baja.");
                return;
            }
            Console.WriteLine($"Dando baja a {bajaExp}");
            for (int i = 0; i < datos.GetLength(1); i++)
            {
                datos[fila, i] = "-1";
            }
        }
        else
        {
            Console.WriteLine("Expediente no encontrado, imposible eliminar...");
        }
    }
    public static void DarBajaLibro(string[,] dato)
    {
        string bajaExp = EntradaDatos("Ingresa el ID a elimianr: ");
        int fila = EncontrarCasillaMatriz(dato, bajaExp);
        if (fila >= 0) //No se encuentra el expediente
        {
            Console.WriteLine($"Dando baja a {bajaExp}");
            for (int i = 0; i < dato.GetLength(1); i++)
            {
                dato[fila, i] = "-1";
            }
        }
        else Console.WriteLine("Expediente no encontrado, imposible eliminar...");
    }
    public static string[,] CrearMatrizRec(string path)
    {
        string texto = LecturaDocs(path); //Obteniendo el texto del txt
        string[] filas = texto.Split('\n');
        string[,] matriz = new string[filas.Length, 4];
        for (int i = 0; i < filas.Length; i++)
        {
            string[] datosDeFila = filas[i].Split(',');
            for (int j = 0; j < datosDeFila.Length; j++)
            {
                matriz[i, j] = datosDeFila[j].Trim();
            }
        }
        return matriz;
    }
    public static string[,] CrearMatrizPres(string path)
    {
        string texto = LecturaDocs(path); // Obteniendo el texto del txt
        string[] filas = texto.Split('\n');
        string[,] matriz = new string[filas.Length, 7]; // expediente, id, monto por multa, fecha de prestamos, fecha a entregar, devolucion
        for (int i = 0; i < filas.Length; i++)
        {
            string[] datosDeFila = filas[i].Split(',');
            for (int j = 0; j < datosDeFila.Length; j++)
            {
                matriz[i, j] = datosDeFila[j].Trim();
            }
        }
        return matriz;
    }
    public static string[,] CrearMatrizUsuario(string path)
    {
        string texto = LecturaDocs(path); //Obteniendo el texto del txt
        string[] filas = texto.Split('\n');
        string[,] matriz = new string[filas.Length, 7];
        for (int i = 0; i < filas.Length; i++)
        {
            string[] datosDeFila = filas[i].Split(',');
            for (int j = 0; j < datosDeFila.Length; j++)
            {
                matriz[i, j] = datosDeFila[j].Trim();
            }
        }
        /*string texto = LecturaDocs(path); //Obteniendo el texto del txt
        string[] filas = texto.Split('\n');
        string[,] matriz = new string[filas.Length, 4];
        for (int i = 0; i < filas.Length; i++)
        {
            string[] datosDeFila = filas[i].Split(',');
            for (int j = 0; j < 4; j++)
            {
                if (j < datosDeFila.Length)
                {
                    matriz[i, j] = datosDeFila[j].Trim();
                }
                else
                {
                    matriz[i, j] = string.Empty; // o algún valor por defecto
                }
            }
        }*/
        return matriz;
    }
    public static string[,] CrearMatrizLibro(string path)
    {
        string texto = LecturaDocs(path); //Obteniendo el texto del txt
        string[] filas = texto.Split('\n');
        string[,] matriz = new string[filas.Length, 7];
        for (int i = 0; i < filas.Length; i++)
        {
            string[] datosDeFila = filas[i].Split(',');
            for (int j = 0; j < datosDeFila.Length; j++)
            {
                matriz[i, j] = datosDeFila[j].Trim();
            }
        }
        return matriz;
    }
    public static string[,] CrearMatrizAdmin(string path)
    {
        string texto = LecturaDocs(path); //Obteniendo el texto del txt
        string[] filas = texto.Split('\n');
        string[,] matriz = new string[filas.Length, 2];
        for (int i = 0; i < filas.Length; i++)
        {
            string[] datosDeFila = filas[i].Split(',');
            for (int j = 0; j < datosDeFila.Length; j++)
            {
                matriz[i, j] = datosDeFila[j].Trim();
            }
        }
        return matriz;
    }
    public static string[,] CrearMatrizBit(string path)
    {
        string texto = LecturaDocs(path); //Obteniendo el texto del txt
        string[] filas = texto.Split('\n');
        string[,] matriz = new string[filas.Length, 4];
        for (int i = 0; i < filas.Length; i++)
        {
            string[] datosDeFila = filas[i].Split(',');
            for (int j = 0; j < datosDeFila.Length; j++)
            {
                matriz[i, j] = datosDeFila[j].Trim();
            }
        }
        return matriz;
    }
    public static void SolicitarPrestamo(string pathUsuarios, string pathLibros, string pathPrestamos,string[,] datos, string[,] datosLib, string[,] datosPres)
    {
        string userID = EntradaDatos("Ingresa el expediente");
        int userIndex = EncontrarCasillaMatriz(datos, userID);
        bool flagStock = true;
        if (userIndex == -1)
        {
            Console.WriteLine("Usuario no encontrado.");
            flagStock = false;
        }
        // Verificar si el usuario ya tiene un libro prestado
        for (int i = 0; i < datosPres.GetLength(0); i++)
        {
            if (datosPres[i, 0] == userID && datosPres[i, 5] == "0") //1/0
            {
                Console.WriteLine("Ya tienes un libro prestado.");
            }
        }
        string bookID = EntradaDatos("Ingresa el ID del libro que deseas prestar: ");
        int bookIndex = EncontrarCasillaMatriz(datosLib, bookID);
        flagStock=true;
        if (bookIndex == -1)
        {
            Console.WriteLine("Libro no encontrado.");
            flagStock = false;
        }
        int stock = int.Parse(datosLib[bookIndex, 6]); //tryparse
        flagStock = true;
        if (stock <= 0)
        {
            Console.WriteLine("No hay ejemplares disponibles para prestar.");
             flagStock= false;
        }
        // Solicitar número de días para el préstamo
        string diasPrestamoStr = EntradaDatos("¿Por cuántos días deseas hacer el préstamo?");
        flagStock = true;
        if (!int.TryParse(diasPrestamoStr, out int diasPrestamo) || diasPrestamo <= 0)
        {
            Console.WriteLine("Número de días inválido.");
            flagStock= false;
        }
        DateTime fechaActual = DateTime.Now;
        DateTime fechaEntrega = fechaActual.AddDays(diasPrestamo);
        // Registrar el préstamo
        for (int i = 0; i < datosPres.GetLength(0); i++)
        {
            if (string.IsNullOrEmpty(datosPres[i, 0]))
            {
                datosPres[i, 0] = userID;
                datosPres[i, 1] = bookID;
                datosPres[i, 2] = "0"; // Monto de multa inicial
                datosPres[i, 3] = fechaActual.ToString("yyyy-MM-dd");
                datosPres[i, 4] = fechaEntrega.ToString("yyyy-MM-dd");
                datosPres[i, 5] = "0"; // No devuelto
                datosPres[i, 6] = "0"; // No transferido
                break;
            }
        }
        // Reducir el stock del libro
        datosLib[bookIndex, 6] = (stock - 1).ToString(); 
        // Guardar los cambios
        EscribirCambios(pathLibros, datosLib);
        EscribirCambiosAlta(pathPrestamos, datosPres);
        Console.WriteLine($"Préstamo realizado con éxito. Debes devolver el libro antes del {fechaEntrega:dddd, dd MMMM yyyy}.");
    }
    public static void DevolverLibro(string pathUsuarios, string pathLibros, string pathPrestamos, string[,] datosPres, string[,] datosLib)
    {
        string userID = EntradaDatos("Ingresa el expediente");
        int userIndex = EncontrarCasillaMatriz(datosPres, userID);
        if (userIndex == -1)
        {
            Console.WriteLine("Usuario no encontrado en la base de datos de préstamos.");
            return;
        }

        for (int i = 0; i < datosPres.GetLength(0); i++)
        {
            if (datosPres[i, 0] == userID && datosPres[i, 5] == "0")
            {
                string bookID = datosPres[i, 1];
                DateTime fechaEntrega = DateTime.Parse(datosPres[i, 4]);
                DateTime fechaActual = DateTime.Now;

                int diasRetraso = (fechaActual - fechaEntrega).Days;
                if (diasRetraso > 0)
                {
                    double montoMultaInicial = double.Parse(datosPres[i, 2]);
                    double multaAcumulada = montoMultaInicial=(diasRetraso * 15);
                    Console.WriteLine($"El libro está retrasado {diasRetraso} días. Debes pagar una multa de ${multaAcumulada}.");
                    datosPres[i, 2] = multaAcumulada.ToString(); // Actualizar la multa acumulada en la base de datos
                }
                else
                {
                    Console.WriteLine("Libro devuelto a tiempo.");
                }

                // Actualizar el estado del préstamo
                datosPres[i, 5] = "1"; // Indicar que el libro ha sido devuelto

                // Incrementar el stock del libro
                int bookIndex = EncontrarCasillaMatriz(datosLib, bookID);
                if (bookIndex >= 0)
                {
                    int stock = int.Parse(datosLib[bookIndex, 6]);
                    datosLib[bookIndex, 6] = (stock + 1).ToString(); // Incrementar el stock
                }

                // Guardar los cambios en las bases de datos
                EscribirCambios(pathLibros, datosLib);
                EscribirCambios(pathPrestamos, datosPres);

                return;
            }
        }

        Console.WriteLine("No se encontraron préstamos activos para este usuario.");
    }
    public static void VerificarMultasYDiasRestantes(string pathPrestamos, string[,] datosPres)
    {
        string userID = EntradaDatos("Ingresa el expediente");
        int userIndex = EncontrarCasillaMatriz(datosPres, userID);
        if (userIndex == -1)
        {
            Console.WriteLine("Expediente no existente");
            return;
        }

        bool prestamosActivos = false;
        for (int i = 0; i < datosPres.GetLength(0); i++)
        {
            if (datosPres[i, 0] == userID && datosPres[i, 5] == "0") // Verificar que el libro no ha sido devuelto
            {
                prestamosActivos = true;
                DateTime fechaLimite = DateTime.Parse(datosPres[i, 4]);
                DateTime fechaInicio = DateTime.Parse(datosPres[i, 3]);
                DateTime fechaActual = DateTime.Now;

                int diasRestantes = (fechaLimite - fechaActual).Days;
                if (diasRestantes < 0)
                {
                    diasRestantes = 0; // No hay días restantes si ya pasó la fecha de entrega
                }
                Console.WriteLine($"Días restantes: {diasRestantes}");

                int diasRetraso = (fechaActual - fechaLimite).Days;
                if (diasRetraso > 0)
                {
                    double montoMultaInicial = double.Parse(datosPres[i, 2]);
                    double multaAcumulada = montoMultaInicial=(diasRetraso * 15); // Sumar la multa inicial más la multa diaria por retraso
                    Console.WriteLine($"El préstamo está atrasado {diasRetraso} días. Multa acumulada: ${multaAcumulada}");
                }
                else
                {
                    Console.WriteLine("No hay multa acumulada. El libro está a tiempo.");
                }
                return;
            }
        }

        if (!prestamosActivos)
        {
            Console.WriteLine("No se encontraron préstamos activos.");
        }
    }
    public static void TransferirPrestamo(string pathUsuarios, string pathLibros, string pathPrestamos, string[,]datos, string[,] datosPres, string[,]datosBit,string pathAdmin, string pathBit,string pathRecomendaciones, string[,]datosLib, string[,]datosRec)
    {

    }
    public static bool MultasActivas(string expediente, string[,] datosPres)
    {
        for (int i = 0; i < datosPres.GetLength(0); i++)
        {
            if (datosPres[i, 0] == expediente && datosPres[i, 5] == "0") // Verificar que no ha sido devuelto
            {
                return true;
            }
        }
        return false;
    }
    public static int EncontrarCasillaMatriz(string[,] arreglo, string expediente)
    {
        for (int i = 0; i < arreglo.GetLength(0); i++)
        {
            if (arreglo[i, 0].Trim() == expediente.Trim())
            {
                return i;
            }
        }
        return -1;
    }
    public static void CambiarDatoMatriz(string exp, string[,] arregloPalabras, string path, int x)
    {
        //Buscar al expediente que vamos a modificar
        int filaUsuario = -1;
        filaUsuario = EncontrarCasillaMatriz(arregloPalabras, exp); //va dar un número > 0 si existe le user
        if (filaUsuario >= 0)
        {
            string entrada = EntradaDatos("Ingresa el nuevo valor: ");
            arregloPalabras[filaUsuario, x] = entrada;
            EscribirCambios(path, arregloPalabras);
        }
        else
        {
            Console.WriteLine("Usuario no encontrado");
        }
    }
    public static void Filtrado(string[,] datos, string filtro, int x)
    {
        for (int i = 0; i < datos.GetLength(0) - 1; i++)
        {
            if (datos[i, x].ToLower().Contains(filtro.ToLower()))
            {
                for (int j = 0; j < datos.GetLength(1); j++)
                {
                    Console.Write(datos[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }
    public static void ImprimirMatriz(string[,] arreglo)
    {
        for (int i = 0; i < arreglo.GetLength(0); i++)
        {
            for (int j = 0; j < arreglo.GetLength(1); j++)
            {
                Console.Write(arreglo[i, j]);
                Console.Write(" ");
            }
            Console.WriteLine();

        }
    }
    public static void VerificaDoc(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("Los archivos no existen,+" +
                "creando Documentacion necesaria...");
            CrearDocs(path);
            Console.WriteLine("Documentacion Creada con exito");
        }
    }
    public static void CrearDocs(string path)
    {
        StreamWriter crear = File.CreateText(path);
        crear.WriteLine("Bienvenido a LoveBooks");
        crear.Close();
    }
    public static string LecturaDocs(string path)
    {
        string texto;
        StreamReader lectura = File.OpenText(path);
        texto = lectura.ReadToEnd();
        lectura.Close();
        return texto;
    }
    public static string EntradaDatos(string texto)
    {
        Console.WriteLine(texto);
        return Console.ReadLine();
    }
    public static void Stock(string path, string[,] datosLib)
    {
        Console.Clear();
        Console.WriteLine("Añadir Stock");
        string id = EntradaDatos("Ingrese el ID del libro para añadir stock: ");
        int cantidad;
        if (!int.TryParse(EntradaDatos("Ingrese la cantidad de libros a añadir: "), out cantidad))
        {
            Console.WriteLine("Error: Debe ingresar un número entero válido.");
            return;
        }
        int fila = EncontrarCasillaMatriz(datosLib, id);
        if (fila >= 0)
        {
            int stockActual;
            if (int.TryParse(datosLib[fila, 6], out stockActual))
            {
                datosLib[fila, 6] = (stockActual + cantidad).ToString();
                Console.WriteLine($"Se añadieron {cantidad} libros al stock del libro con ID {id}.");
                EscribirCambiosAlta(path, datosLib);
            }
            else
            {
                Console.WriteLine("Error: No se pudo obtener el stock actual del libro.");
            }
        }
        else
        {
            Console.WriteLine("Error: No se encontró ningún libro con el ID especificado.");
        }
    }
}