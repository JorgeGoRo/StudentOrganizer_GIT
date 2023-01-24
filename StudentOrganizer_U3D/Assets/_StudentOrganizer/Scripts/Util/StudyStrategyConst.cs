using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StudyStrategyConst {
    public const string PARAPHRASING_TITLE = "<b>Parafraseo: explicar con mis palabras</b>";

    public const string PARAPHRASING = "El parafraseo, es decir, la explicación con nuestras propias palabras de algo que hemos visto, leído u oído previamente ayuda a entender la información, " +
                                       "ya que tenemos que realizar un ejercicio de abstracción con la información recibida y quedarnos con la idea general de cara a comunicarla de forma fácil y sencilla para que se entienda.\n\n" +
                                       "Actividades de creación de contenido pueden servir para parafrasear, siempre que el contenido creado consista en explicar una idea o tema. " +
                                       "Puede tratarse de contenido escrito tipo post en un blog, para lo que podemos utilizar Blogger, de contenido auditivo tipo Podcast, para lo que podemos utilizar Audacity, o de contenido audiovisual tipo vídeo, " +
                                       "para lo que podemos usar la opción abierta PiTiVi para editar el vídeo en Linux, WeVideo o cualquiera similar si no disponemos de Linux; y posteriormente visualizarlo en un reproductor con VLC Player.\n";

    public const string ANALOGIES_TITLE = "<b>Crear analogías (casos similares)</b>";

    public const string ANALOGIES =
        "Crear analogías es otra estrategia que podemos utilizar para elaborar conocimiento. Las analogías son elaboraciones que realizamos a partir de la similitud entre dos temas o conceptos, es decir, " +
        "la existencia de determinados atributos o características de un tema o concepto resulta similar a los atributos de otro concepto conocido. Al comparar ambos conceptos, nos resulta mucho más sencillo entender la estructura del concepto nuevo que estamos aprendiendo.\n\n" +
        "Una analogía implica relaciones de lo particular a lo particular, a diferencia del razonamiento inductivo que va de lo particular a lo general (un salmón vive en el mar, así que es un pez) y del razonamiento deductivo que va de lo general a lo particular" +
        " (los peces viven en el mar, así que un pez espada vive en el mar). En una analogía partimos de un ejemplo: el salmón vive en el mar; hacia otro: el pez payaso también.\n\n" +
        "Podemos trabajar analogías a través de actividades de reflexión y representación de los conceptos, por ejemplo, intentar completar una lista de frases a partir de un punto común. " +
        "Por ejemplo, enseñamos un ejemplo visual de un banco de imágenes como el del INTEF de una profesión concreta y ellos deben completar otras que se les ocurra: un maestro enseña, igual que un…(bombero, panadero, secretario,…hace X).\n";

    public const string EXAMPLES_TITLE = "<b>Generar inferencias (ejemplos aplicados)</b>";

    public const string EXAMPLES = "Generar inferencias es otra dinámica adecuada para trabajar estrategias de elaboración, ya que consiste en razonar para comprender información que no está de forma explícita en un texto o una imagen. " +
                                   "Para generar inferencias tenemos que unir la información que recibimos con el conocimiento que ya tenemos, de forma que podemos deducir otros aspectos. Requiere establecer conexiones lógicas, ir más allá de lo literal y hacer uso de nuestra experiencia.\n\n" +
                                   "Por ejemplo, si un texto dice “Pablo lleva jugando una hora con un patito de plástico y se da cuenta que tiene los dedos arrugados”, " +
                                   "a partir del conocimiento que ya tengo sobre que los dedos se arrugan cuando se está dentro del agua mucho tiempo, se deduce que Pablo está en su bañera.\n\n" +
                                   "Para ello, podemos trabajar a través de imágenes, de un banco de imágenes como el del INTEF, o a través de textos o frases. " +
                                   "Para generar adecuadamente las inferencias, los alumnos pueden crear una infografía en Canva para distinguir 3 partes que ayuden a reflexionar: Qué veo/leo, Qué sé ya (de ese contexto), Qué deduzco.\n";


    public const string TAKING_NOTES_TITLE = "<b>Tomar notas (resaltar aspectos relevantes)</b>";

    public const string TAKING_NOTES = "Tomar notas puede ser una gran estrategia, ya que al tomarlas no podemos apuntar literalmente todo lo que se dice o todo lo que se ve. Las notas contienen la información relevante, " +
                                       "redactada de una forma clave para que la persona que toma la nota lo entienda. En cuanto a las herramientas TIC que podemos utilizar para tomar notas, destacan Evernote o Google Keep, o el uso de murales como Padlet.\n\n" +
                                       "Otra estrategia puede ser la de responder preguntas, siempre que éstas sean de tipo reflexivo y les ayude a establecer conexiones lógicas e interpretar un contenido. Preguntas que sirvan para establecer causa-consecuencia, " +
                                       "motivos principales para que algo suceda, etc. pueden realizarse en herramientas tipo blog, como Blogger.\n";


    public const string RESUME_TITLE = "<b>Crear resúmenes</b>";

    public const string RESUME = "Efectuar resúmenes suele ser la estrategia más utilizada para elaborar conocimiento, y es que, de forma similar a las notas, el resumen debe contener la información relevante redactada de forma clave. " +
                                 "Se pueden utilizar herramientas de elaboración de textos como Word o Google Docs, haciendo uso de listas, p.e. viñetas.\n";


    public const string SORT_OUT_TITLE = "<b>Hacer clasificaciones por categorías</b>";
    public const string SORT_OUT = "Realizar clasificaciones es una estrategia clásica de organización que permite distinguir los elementos sustancialmente diferentes de un concepto para dividirlo en categorías con significado.\n";


    public const string CONCEPTUAL_MAP_TITLE = "<b>Crear mapas conceptuales</b>";

    public const string CONCEPTUAL_MAP =
        "Los mapas conceptuales son otra estrategia de organización, ya que permiten establecer las relaciones existentes entre varios conceptos, además de poder mostrar de forma visual una jerarquía entre conceptos. " +
        "Un mapa conceptual se puede ir actualizando según se profundiza en un tema, no debe verse como un instrumento puntual a utilizar en un momento dado del aprendizaje. Lo ideas es empezar a utilizarlo tras la comprensión del conocimiento, y continuar hasta el final del tema.\n\n" +
        "Existen diversas herramientas TIC específicas para la creación de los mapas conceptuales. Entre ellas, hay que destacar mindmeister y pooplet, las cuales se utilizan a través del navegador y tienen la opción de trabajar de forma colaborativa.\n";


    public const string SCHEME_TITLE = "<b>Elaborar esquemas</b>";

    public const string SCHEME =
        "Los esquemas están a medio camino entre los resúmenes y los mapas conceptuales. Sirven para organizar la información de manera visual, pero añadiendo (respecto a los mapas conceptuales) una síntesis lógica del contenido. Por tanto, se pueden ver las relaciones entre conceptos (organización) y sus ideas clave (elaboración). \n\n" +
        "Existen diferentes tipos de esquemas:\n\n" +
        "• El esquema de llaves: cada llave incluye determinadas ideas de la misma categoría, que a su vez incluyen otras llaves con ideas de categorías inferiores. La información se lee de izquierda a derecha, por lo que a la izquierda aparecen las ideas principales, y hacia la derecha las ideas subordinadas.\n\n" +
        "• El esquema numérico: incide más en el orden jerárquico de las ideas, poniendo las principales arriba de forma numerada (1, 2, 3,…), y las subordinadas debajo incluyendo el número principal (1.1, 1.2, 1.3,…).\n\n" +
        "• El cuadro sinóptico: representa una visión global de ideas interrelacionadas a través de un cuadro de doble entrada. Facilita la visión de la estructura de relación y su interdependencia.\n\n" +
        "Entre las herramientas TIC que podemos utilizar para realizar esquemas, destaca la facilidad de los software de presentación, tipo PowerPoint o Google Slides, ya que se puede añadir rápidamente cuadros de texto, flechas, llaves,...además de que se ofrecen elementos rápidos de tipos de esquemas (de secuencia, jerárquicos, etc.).\n";
}