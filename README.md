# Prueba Técnica

## Componente Teórico

__1.__ Definiciones de clase, interfáz, método y objeto:

__1.1. Clase:__ En el paradigma de la programación orientada a objetos _(POO)_, una clase es una estructura en la cual se definen atributos y comportamientos de un objeto de la vida real. Un ejemplo es un vehículo, algunos de sus atributos son: número de llantas, marca, número de placa. Algunos comportamientos pueden ser: encender, acelerar y frenar.

```c#
public class Vehiculo {
    
    // Atributos
    public int NumeroLlantas {get; set;}
    public string Marca {get; set;}
    public string NumeroPlaca {get; set;}
    
    // Comportamientos
    public void Encender() {
        Console.WriteLine("Encendiendo vehículo");
    }
    
    public void Acelerar() {
        Console.WriteLine("Acelerando vehículo");
    }
    
    public void Frenar() {
        Console.WriteLine("Frenando vehículo");
    }
}
```

__1.2. Interfáz:__ Una interfáz es una clase abstracta, la diferencia es que en una interfáz se definen únicamente los comportamientos (sin implementaciones) y atributos constantes de una clase. Los métodos allí definidos son extrictamente públicos y la clase que implementa una interfáz debe implementar todos aquellos métodos allí definidos.

```c#
public interface IVehiculo {
	// Comportamientos
	public void Encender();
	public void Acelerar();
	public void Frenar();
}
```

__1.3. Método:__ Un método es la implementación de un algoritmo, consta de un inicio, fin y una serie de pasos para lograr un fin específico, puede incluir condicionales, bucles y llamado a otros métodos. Para definir un método se debe especificar su nivel de encapsulamiento, tipo de valor que retorna, nombre y parámetros.

```c#
public int Sumar(int x, int y) {
	return x + y;
}
```

__1.4. Objeto:__ Es la instancia de una clase, se crea un espacio en memoria y puede hacer uso de los atributos y comportamentos definidos en la clase.

```c#
Vehiculo carro = new Vehiculo();
```

__2. Inyección de dependencias:__ La inyección de dependencias se usa principalmente para dar responsabilidad al framework de llevar a cabo la distribución del tiempo de vida de las variables de la aplicación, con el fin de optimizar los gastos referentes a los recursos hardware. En .NET Core existen 3 tipos de tiempo de vida: transient, scoped y singleton. Se implementa generalmente por medio de interfaces y clases.

__3. Comentarios de código:__ 

* Haría un comentario por la organización del código, no esta correctamente tabulado, esto lo hace un poco más dificil de leer.

* En versiones recientes de C# las sentencias `using` se pueden establecer sin la necesidad de usar llaves `{}`, sin tener algun tipo de impacto en el funcionamiento de las variables. Con el fin de que el código sea mas limpio y fácil de entender.

* Evitar declarar variables usando la palabra clave `var`, en lo posible declarar variables especificando su correspondiente tipo para interpretar mejor el funcionamiento del código.

* Verificar previamente el uso del método `EnsureSuccessStatusCode` para validar que la petición `HTTP` retorno un código de estado satisfactorio, de lo contrario manejar el error.

* Los métodos `reader.Close()`, `reader.Dispose()`, `response.Close()` y `response.Dispose()` son irrelevantes en esta implementación, ya que las variables `reader` y `response` se declararon de tipo `disposable`, esto significa que al momento de finalizar el bloque de código se borrarán de memoria automáticamente.

* La variable `result` no debería ser de tipo `dynamic`, luego de recibir la respuesta de la petición `HTTP`, únicamente se retorna el resultado, por esto el uso de las variable de tipo `dynamic` en este punto pierde sentido, establecer el tipo de variable concreto y retornar.

__4. Indices en las bases de datos:__ Los índices ayudan a mejorar los tiempos de respuesta al momento de obtener registros de las tablas de la base de datos, principalmente en tablas con una cantidad considerable de datos. La declaración de los índices debe realizarse únicamente en tablas con un alto volumen de datos y con una transaccionalidad bastante alta, seleccionando estratégicamnente los campos en los cuales la implementación de estos indices impacten de manera positiva las consultas de búsqueda, por lo general, los campos con los cuales se filtra bastante la información.

## Componente Práctico Backend

La `API` ha sido realizada en `.NET Core 6` y el motor de base de datos usado es una instancia en la nube de `SQL Server 2019`.

### Web API

La `solucion` del proyecto esta en: `ParkingApp/ParkingApp.sln`

En el `API` se hizo uso de librerias como `Dapper` para la conexion contra la base de datos, `AutoMapper` para facilitar la conversion entre objetos de tipo entidad y DTOs, `ModelValidation` para las validaciones de los valores de entrada al `API`, Filtro de excepciones para llevar un mejor control de las excepciones lanzadas en la aplicacion, inyeccion de dependencias, arquitectura por capas, codigo limpio y documentacion.

### Base de datos

La `base de datos` consta 3 `tablas` en el esquema `dbo`:

* __VehicleTypes__: Almacena los tipos de vehiculos y tarifas __por minuto__ discriminadas en este ejercicio: Bicicleta $10, Motocicleta $50, Carro $110.
* __Vehicles__: Almacena los distintos vehiculos que se van registrando en la aplicacion, con el fin de poder llegar a tener un historico por vehiculo y de esta manera llevar control mas completo del parqueadero. Se guarda el numero de placa y el tipo del vehiculo.
* __Logs__: Almacena los registros del parqueadero. Se almacena la fecha de entrada, fecha de salida, valor pagado, tiempo de parqueo, numero de la factura del supermercado (en caso de ser necesario) y se hace referencia al vehiculo ingresado.

___Nota:__ Si se desea conocer los scripts utilizados para la creacion de las `tablas` se pueden encontrar en:_ `Database/Tables.sql`

Se crearon 4 `procedimientos almacenados` con el fin de realizar el CRUD de la informacion almacenada en las `tablas`.

___Nota:__ Si se desea conocer los scripts utilizados para la creacion de los `procedimientos almacenados` se pueden encontrar en:_ `Database/StoredProcedures.sql`

### Keywords

| Nombre | Descripcion | Tipo de dato | Observaciones | Ejemplo |
| ------ | ----------- | ------------ | ------------- | ------- |
| `id` | ID del registro de parqueo | Guid | | 0f8fad5b-d9cb-469f-a165-70867728950e |
| `entry` | Fecha de entrada de un vehiculo al parqueadero | DateTime | | 2023-05-15T12:30:00 |
| `departure` | Fecha de salida de un vehiculo del parqueadero | DateTime | | 2023-05-15T13:45:00 |
| `price` | Valor pagado segun la fecha de ingreso y salida, tipo de vehiculo y descuento (si aplica) | float | | 15000 |
| `time` | Tiempo de parqueo del vehiculo | string | `d` dias, `h` horas y `m` minutos | 1d 12h 30m |
| `billDiscountNumber` | Numero de la factura del supermercado para aplicar descuento en la tarifa | string | 8 caracteres | 12345678
| `vehicle.numberPlate`** | Numero de placa del vehiculo | string | 6 caracteres | ABC123 |
| `vehicle.type.id` | ID del tipo de vehiculo | byte | `1` bicicleta, `2` motocicleta y `3` carro | 3 |
| `vehicle.type.name` | Nombre del tipo de vehiculo | byte | | Bicycle |
| `PageIndex` | Indice de la pagina a buscar | byte | `n` >= 1 | 1 |
| `ItemsCount` | Cantidad de items que se desea ver por pagina | byte | `n` >= 1 | 1 |

___**Nota:__ El numero de placa debe cumplir las siguientes condiciones: `Bicicleta` 6 numeros ej. 123456, `Motocicleta` 3 letras mayusculas + 2 numeros + 1 letra mayuscula ej. ABC12D y `Carro` 3 letras mayusculas + 3 numeros ej. ABC123_

### Endpoints

A continuacion se hace un breve recuento de los endpoints del `API`. Recordar cambiar el valor de `HOST` en cada uno de los `curl`. Recomiendo usar `postman` para realizar las pruebas de funcionamiento.

* __POST Registrar ingreso:__ Se envia la fecha de entrada, numero de placa y tipo de vehiculo

```bash
curl --location '<HOST>/logs' \
--header 'accept: */*' \
--header 'Content-Type: application/json' \
--data '{
  "entry": "2023-05-15T12:00:00",
  "vehicle": {
    "numberPlate": "ABC123",
    "type": {
      "id": 3
    }
  }
}'
```

___Nota:__ Retorna un codigo `201` si todo sale bien_

* __PUT Liquidar salida:__ Se envia el numero de placa del vehiculo, fecha de salida y numero de la factura del supermercado en caso de ser necesario

```bash
curl --location --request PUT '<HOST>/logs?numberPlate=ABC123&departure=2023-05-15T13:30:00&billDiscountNumber=12345678
```

___Nota:__ Retorna un codigo `200` si todo sale bien_

* __GET Obtener listado de vehiculos:__ Dado que la informacion se retorna de forma paginada se debe enviar el numero de pagina, cantidad de items a mostrar, fecha de entrada y salida

```bash
curl --location '<HOST>/logs?PageIndex=1&ItemsCount=5&Entry=2023-05-15&Departure=2023-05-16'
```

___Nota:__ Retorna un codigo `200` si todo sale bien y la lista de registros de parqueadero paginada_
