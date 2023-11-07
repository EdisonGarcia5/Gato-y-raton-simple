using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gato
{
    public partial class Form1 : Form
    {
        // variables a utilizar en el codigo.
        Button[] buttons;

        int puntosGato = 0;
        int puntosRaton = 0;

        bool Turno = true;
        int movimientos = 0;
        bool jugando = true;

        public Form1()
        {
            InitializeComponent();

            // Activa los botones con un Arreglo anidado para almacenar los botones 
            buttons = new Button[]
            {
            button1, button2, button3,
            button4, button5, button6,
            button7, button8, button9
            };
            
            
            foreach (Button button in buttons)
            {
                button.Enabled = false; // Desactivar los botones del tablero
                button.Click += BotonClick; // lee la clase y la utiliza en el form1
            }
            
        }


        // Sector 1
        // Clase donde se aplica la logica del juego.
        private void BotonClick(object sender, EventArgs e)
        {
            Button boton = (Button)sender; // se obtiene el botón que generó el evento a través del parámetro "sender" (obtene el objeto que envió el evento).

            if (jugando) // se verifica si se está jugando y ya esta activado como verdadero para iniciar.
            {
                if (Turno) // verifica el turno actual
                {
                    boton.Text = "X"; // jugador 
                }
                else
                {
                    boton.Text = "O"; // maquina
                }

                Turno = false;
                movimientos++; //lee los espacios disponibles.
                
                if (Combinaciones()) // se verifica si se ha alcanzado una combinación ganadora llamando a la función.
                {
                    // se utiliza la variable ganador para indicar el mensaje correspondiente y actualizar la etiqueta.
                    string ganador;
                    if (!Turno)
                    {
                        ganador = "Gato";
                        puntosGato += 1;
                    }
                    else
                    {
                        ganador = "Ratón";
                        puntosRaton += 1;
                    }

                    MessageBox.Show("El ganador es el Usuario (" + ganador + ")");

                    label1.Text = "🐱: " + puntosGato + "    🐭: " + puntosRaton;

                    jugando = false;
                }
                else if (movimientos == 9) // Si no hay una combinación ganadora y se han realizado 9 movimientos, se muestra un mensaje.
                {
                    MessageBox.Show("¡Empate!");
                    jugando = false;
                }

                // se llama a la función "JugarMaquina()" para que la máquina realice su movimiento.
                if (jugando && !Turno)
                {
                    JugarMaquina();
                }
            }

        }


        // Sector 2
        // mejora y bloque las jugadas del usuario 
        private void JugarMaquina()
        {
            // seleccionar aleatoriamente un botón de un arreglo llamado "buttons" y almacenarlo en la variable "boton". 
            Random random = new Random();
            int indice = random.Next(buttons.Length);
            Button boton = buttons[indice];

            while (boton.Text != "") //se verifica si el texto del botón seleccionado es vacío. Si no es vacío, se genera un nuevo número aleatorio y se selecciona otro botón del arreglo. 
            {
                indice = random.Next(buttons.Length);
                boton = buttons[indice];
            }

            // Original 
            // Verificar si el usuario está a punto de ganar en su próximo movimiento
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].Text == "")
                {
                    buttons[i].Text = "X"; // Simular movimiento del usuario
                    if (Combinaciones())
                    {
                        boton = buttons[i]; // Bloquear la jugada del usuario
                        break;
                    }
                    buttons[i].Text = ""; // Deshacer simulación del movimiento del usuario
                }
            }

            // Hacer el movimiento de la máquina
            boton.Text = "O";
            Turno = true;
            movimientos++;

            // Verificar si la máquina ganó o hubo empate
            if (Combinaciones())
            {
                MessageBox.Show("El ganador es la Maquina (Ratón)");
                puntosRaton += 1;
                label1.Text = "🐱: " + puntosGato + "    🐭: " + puntosRaton;
                jugando = false;
            }
            else if (movimientos == 9)
            {
                MessageBox.Show("¡Empate!");
                jugando = false;
            }
        }
        

        // Sector 3
        // Verificar todas las combinaciones ganadoras
        private bool Combinaciones()
        {
            if  (
                (buttons[0].Text == buttons[1].Text && buttons[1].Text == buttons[2].Text && buttons[0].Text != "") ||
                (buttons[3].Text == buttons[4].Text && buttons[4].Text == buttons[5].Text && buttons[3].Text != "") ||
                (buttons[6].Text == buttons[7].Text && buttons[7].Text == buttons[8].Text && buttons[6].Text != "") ||

                (buttons[0].Text == buttons[3].Text && buttons[3].Text == buttons[6].Text && buttons[0].Text != "") ||
                (buttons[1].Text == buttons[4].Text && buttons[4].Text == buttons[7].Text && buttons[1].Text != "") ||
                (buttons[2].Text == buttons[5].Text && buttons[5].Text == buttons[8].Text && buttons[2].Text != "") ||

                (buttons[0].Text == buttons[4].Text && buttons[4].Text == buttons[8].Text && buttons[0].Text != "") ||
                (buttons[2].Text == buttons[4].Text && buttons[4].Text == buttons[6].Text && buttons[2].Text != "") 
                )
            {
                return true;
            }
            return false;
        }


        // Sector 4 botones 
        private void IniciarJuego_Click(object sender, EventArgs e)
        {
            foreach (Button button in buttons)
            {
                button.Enabled = true; // Activar los botones del tablero
            }
        }
        
        private void Nuevojuego_Click(object sender, EventArgs e)
        {
            foreach (Button button in buttons)
            {
                button.Text = "";
            }

            movimientos = 0;
            jugando = true;
            Turno = true;
        }
        
        private void Reiniciar_Click_1(object sender, EventArgs e)
        {
            foreach (Button button in buttons)
            {
                button.Enabled = false; // Desactivar los botones del tablero
                button.Text = "";
            }

            movimientos = 0;

            jugando = true;
            Turno = true;

            puntosGato = 0;
            puntosRaton = 0;

            label1.Text = "🐱: " + puntosGato + "    🐭: " + puntosRaton;
        }

        private void Cerrar_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

    