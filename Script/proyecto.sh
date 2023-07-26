#!/bin/bash
clear

echo -e "\e[32m"

# Definir opciones válidas
VALID_OPTIONS=("run" "clean" "report" "slides" "show_report" "show_slides" "help")

# Verificar si se proporcionó al menos una opción
if [ $# -lt 1 ]; then
    echo "Error: No se proporcionó ninguna opción. Las opciones válidas son: ${VALID_OPTIONS[@]}"
    exit 1
fi

# Verificar si la opción proporcionada es válida
if [[ ! " ${VALID_OPTIONS[@]} " =~ " $1 " ]]; then
    echo "Error: Opción no válida. Las opciones válidas son: ${VALID_OPTIONS[@]}"
    exit 1
fi

# Definir el visor de PDF predeterminado
PDFVIEWER="evince"

# funcion para ejecutar el moogle 
run()
{
   
   echo "Ejecutando Moogle!"
   cd ../ 
   dotnet watch run --project MoogleServer 
     
}
# Funcion para borrar archivos basura de los tex
clean()
{
    clear
    echo "Limpiando archivos basura"

    cd ../ 
    cd Informe 
    rm -f *.log *.aux *.out *.pdf 
    cd ../
    cd Presentacion
    rm -f *.log *.aux *.out *.pdf *.toc *.smn *.nav

    echo "Limpiado"
}
# Funcion para compilar el reporte 
report()
{
    clear
    echo "Compilando reporte"

    cd ../
    cd Informe
    pdflatex -synctex=1 -interaction=nonstopmode informe.tex

    echo "Reporte compilado"

}
# Funcion para compilar la presentacion 
slides()
{
   clear
   echo "Compilando presentacion"
   cd ../
   cd Presentacion
   pdflatex -synctex=1 -interaction=nonstopmode presentacion.tex

}
# Funcion para ejecutar el reporte pdf 
show_report()
{
    clear
    echo "Abriendo informe"
    cd ../
    cd Informe 
    document="informe.pdf"

    if [ ! -f "$document" ]; then 
        report 
    fi

    # Verificar si se especificó un visor de PDF personalizado, si no, usar el predeterminado
    if [ "$2" = "-v" ]; then

        # Verificar si se proporcionó un comando de visor de PDF personalizado, si no, mostrar mensaje de error y salir
        if [ -z "$3" ]; then
            echo "Error: No se proporcionó ningún comando de visor de PDF personalizado. Por favor proporcione un comando válido después de la opción '-v'."
            exit 1
        fi

        $3 $document

    else 

        $PDFVIEWER $document

    fi
   
}
# Funcion para ejecutar la presentacion pdf
show_slides()
{
    clear
    echo "Abriendo presentacion"
    cd ../
    cd Presentacion 
    document="Presentacion.pdf"

    if [ ! -f "$document" ]; then 
        slides 
    fi

    # Verificar si se especificó un visor de PDF personalizado, si no, usar el predeterminado
    if [ "$2" = "-v" ]; then

        # Verificar si se proporcionó un comando de visor de PDF personalizado, si no, mostrar mensaje de error y salir
        if [ -z "$3" ]; then
            echo "Error: No se proporcionó ningún comando de visor de PDF personalizado. Por favor proporcione un comando válido después de la opción '-v'."
            exit 1
        fi

        $3 $document

    else 

        $PDFVIEWER $document

    fi
   
}

# Funcion para mostrar ayuda sobre cada opcion 
help()
{
   clear
   echo "Ayuda:"
   echo "- run: Ejecuta el proyecto Moogle."
   echo "- clean: Elimina archivos auxiliares generados durante la compilación o ejecución del proyecto."
   echo "- report: Compila y genera el PDF del informe del proyecto."
   echo "- slides: Compila y genera el PDF de la presentación del proyecto."
   echo "- show_report [-v VISOR]: Abre el informe del proyecto en un visor de PDF. Si se especifica la opción '-v' seguida de un comando de visor de PDF personalizado, se utilizará ese visor en lugar del predeterminado."
   echo "- show_slides [-v VISOR]: Abre la presentación del proyecto en un visor de PDF. Si se especifica la opción '-v' seguida de un comando de visor de PDF personalizado, se utilizará ese visor en lugar del predeterminado."
}

# Llamar a la función correspondiente en función de la opción proporcionada con cualquier argumento adicional pasado
case "$1" in
    run)
        run "${@:2}"
        ;;
    clean)
        clean "${@:2}"
        ;;
    report)
        report "${@:2}"
        ;;
    slides)
        slides "${@:2}"
        ;;
    show_report)
        show_report "${@:2}"
        ;;
    show_slides)
        show_slides "${@:2}"
        ;;
    help)
        help "${@:2}"
        ;;
esac

echo -e "\e[0m"

