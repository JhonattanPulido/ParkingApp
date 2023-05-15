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