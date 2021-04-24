﻿// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Collections.Generic;
using Proyecto2.Misc;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto2.TranslatorAndInterpreter
{

    // Clase Principal
    class TransFerSentences : AbstractInstruccion
    {

        // Tipo
        public readonly String TransType;

        // Expression
        public readonly AbstractExpression Expression_;

        // Constructor 
        public TransFerSentences(String TransType, AbstractExpression Expression_)
        {

            // Inicializar Valores 
            this.TransType = TransType;
            this.Expression_ = Expression_;

        }

        // Método Ejecutar
        public override object Execute(EnviromentTable Env)
        {

            // Objecto Retorno
            ObjectReturn TypeReturn = null;

            // Verificar Tipo
            if (this.TransType.Equals("Break"))
            {

                // Inicializar 
                TypeReturn = new ObjectReturn("", "")
                {

                    // Setear Opcional 
                    Option = "break"

                };

            }
            else if (this.TransType.Equals("Continue"))
            {

                // Inicializar 
                TypeReturn = new ObjectReturn("", "")
                {

                    // Setear Opcional 
                    Option = "continue"

                };

            }
            else if (this.TransType.Equals("Return"))
            {

                // Inicializar 
                TypeReturn = Expression_.Execute(Env);

                // Setear Opcional 
                TypeReturn.Option = "return";

            }

            // Retornar 
            return TypeReturn;

        }

        // Método Traducir
        public override object Translate(EnviromentTable Env)
        {

            // Verificar Tipo 
            if (this.TransType.Equals("Break"))
            {

                // Agregar Trauddcion
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "break;\n";

            }
            else if (this.TransType.Equals("Continue"))
            {

                // Agregar Trauddcion
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "continue;\n";

            }
            else if (this.TransType.Equals("Return"))
            {

                // Agregar Trauddcion
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "exit(";

                // Traducir 
                this.Expression_.Translate(Env);

                // Agregar Trauddcion
                VariablesMethods.TranslateString += ");\n";

            }

            // Retornar 
            return null;

        }

        // Método Compilar
        public override object Compilate(EnviromentTable Env)
        {

            // Instancia 
            ThreeAddressCode Instance_1 = ThreeAddressCode.GetInstance;

            // Buscar Etiquetas 
            String[] AuxiliaryArray = Env.SearchBreakAndContinueCycles();

            // Verificar Tipo De Sentencia 
            if (this.TransType.Equals("Break"))
            {

                // Agregar Comentario 
                Instance_1.AddCommentOneLine("Instrucción Break", "Uno");

                // Agregar Salto 
                Instance_1.AddNonConditionalJump(AuxiliaryArray[0], "Dos");

            }
            else if(this.TransType.Equals("Continue")) 
            {

                // Agregar Comentario
                Instance_1.AddCommentOneLine("Instrucción Continue", "Uno");

                // Agregar Salto 
                Instance_1.AddNonConditionalJump(AuxiliaryArray[1], "Dos");

            }
           
            // Retornar 
            return null;

        }

    }

}