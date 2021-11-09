/*
 *  DISCLAIMER: Este código foi criado para discussão e edição durante as aulas práticas de DSS, representando
 *  uma solução em construção. Como tal, não deverá ser visto como uma solução canónica, ou mesmo acabada.
 *  É disponibilizado para auxiliar o processo de estudo. Os alunos são encorajados a testar adequadamente o
 *  código fornecido e a procurar soluções alternativas, à medida que forem adquirindo mais conhecimentos.
 */
package uminho.dss.turmas3l.ui;

import uminho.dss.turmas3l.business.*;

import java.util.Collection;
import java.util.List;
import java.util.Scanner;
import java.util.stream.Collectors;

/**
 * Exemplo de interface em modo texto.
 *
 * @author JFC
 * @version 20210930
 */
public class TextUI {
    // O model tem a 'lógica de negócio'.
    private ITurmasFacade model;

    // Scanner para leitura
    private Scanner scin;

    /**
     * Construtor.
     *
     * Cria os menus e a camada de negócio.
     */
    public TextUI() {

        this.model = new TurmasFacade();
        scin = new Scanner(System.in);
    }

    /**
     * Executa o menu principal e invoca o método correspondente à opção seleccionada.
     */
    public void run() {
        System.out.println("Bem vindo ao Sistema de Gestão de Turmas!");
        this.menuPrincipal();
        System.out.println("Até breve...");
    }

    // Métodos auxiliares - Estados da UI

    /**
     * Estado - Menu Principal
     *
     * Transições para:
     *      Operações sobre Alunos
     *      Operações sobre Turmas
     *      Adicionar Aluno a Turma
     *      Remover Aluno de Turma
     *      Listar Alunos de Turma
     */
    private void menuPrincipal() {
        Menu menu = new Menu(new String[]{
                "Operações sobre Alunos",
                "Operações sobre Turmas",
                "Adicionar Aluno a Turma",
                "Remover Aluno de Turma",
                "Listar Alunos de Turma"
        });

        // Registar pré-condições das transições
        menu.setPreCondition(3, ()->this.model.haAlunos() && this.model.haTurmas());
        // mais pré-condições?

        // Registar os handlers das transições
        menu.setHandler(1, ()->gestaoDeAlunos());
        menu.setHandler(2, ()->gestaoDeTurmas());
        menu.setHandler(3, ()->adicionarAlunoATurma());
        menu.setHandler(4, ()->removerAlunoDeTurma());
        menu.setHandler(5, ()->listarAlunosDaTurma());

        // Executar o menu
        menu.run();
    }

    /**
     *  Estado - Gestão de Alunos
     *
     *  Transições para:
     *      Gestão de Alunos
     *      Adicionar Aluno
     *      Consultar Aluno
     *      Listar Alunos
     */
    private void gestaoDeAlunos() {
        // Implementar
        Menu menu = new Menu(new String[]{
                "Adicionar aluno",
                "Consultar aluno",
                "Listar alunos"
        });

        menu.setPreCondition(2,()->this.model.haAlunos());

        menu.setHandler(1, ()->adicionarAluno());
        menu.setHandler(2, ()->consultarAluno());
        menu.setHandler(3, ()->listarAlunos());

        menu.run();
    }

    /**
     * Estado - Gestão de Turmas
     *
     * Transições possíveis para:
     *      Gestão de Turmas
     *      Adicionar Turma
     *      Mudar Sala à Turma
     *      Listar Turmas
     */
    private void gestaoDeTurmas() {
        // Implementar

        Menu menu = new Menu(new String[]{
                "Adicionar turma",
                "Mudar sala a turma",
                "Listar turmas"
        });

        menu.setPreCondition(2,()->this.model.haTurmas());

        menu.setHandler(1, ()->adicionarTurma());
        menu.setHandler(2, ()->mudarSalaDeTurma());
        menu.setHandler(3, ()->listarTurmas());

        menu.run();
    }

    /**
     *  Estado - Adicionar Aluno
     */
    private void adicionarAluno() {
        try {
            System.out.println("Número da novo aluno: ");
            String num = scin.nextLine();
            if (!this.model.existeAluno(num)) {
                System.out.println("Nome da novo aluno: ");
                String nome = scin.nextLine();
                System.out.println("Email da novo aluno: ");
                String email = scin.nextLine();
                this.model.adicionaAluno(new Aluno(num, nome, email));
                System.out.println("Aluno adicionado");
            } else {
                System.out.println("Esse número de aluno já existe!");
            }
        }
        catch (NullPointerException e) {
            System.out.println(e.getMessage());
        }
    }

    /**
     *  Estado - Consultar Aluno
     */
    private void consultarAluno() {
        try {
            System.out.println("Número a consultar: ");
            String num = scin.nextLine();
            if (this.model.existeAluno(num)) {
                System.out.println(this.model.procuraAluno(num).toString());
            } else {
                System.out.println("Esse número de aluno não existe!");
            }
        }
        catch (NullPointerException e) {
            System.out.println(e.getMessage());
        }
    }

    /**
     *  Estado - Listar Alunos
     */
    private void listarAlunos() {
        try {
            System.out.println(this.model.getAlunos().toString());
        }
        catch (NullPointerException e) {
            System.out.println(e.getMessage());
        }
    }

    /**
     *  Estado - Adicionar Turma
     */
    private void adicionarTurma() {
        try {
            System.out.println("Número da turma: ");
            String tid = scin.nextLine();
            if (!this.model.existeTurma(tid)) {
                System.out.println("Sala: ");
                String sala = scin.nextLine();
                System.out.println("Edifício: ");
                String edif = scin.nextLine();
                System.out.println("Capacidade: ");
                int cap = scin.nextInt();
                scin.nextLine();    // Limpar o buffer depois de ler o inteiro
                this.model.adicionaTurma(new Turma(tid, new Sala(sala, edif, cap)));
                System.out.println("Turma adicionada");
            } else {
                System.out.println("Esse número de turma já existe!");
            }
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }
    }

    /**
     *  Estado - Mudar Sala de Turma
     */
    private void mudarSalaDeTurma() {
        try {
            System.out.println("Número da turma: ");
            String tid = scin.nextLine();
            if (this.model.existeTurma(tid)) {
                System.out.println("Sala: ");
                String sala = scin.nextLine();
                System.out.println("Edifício: ");
                String edif = scin.nextLine();
                System.out.println("Capacidade: ");
                int cap = scin.nextInt();
                scin.nextLine();    // Limpar o buffer depois de ler o inteiro
                this.model.alteraSalaDeTurma(tid, new Sala(sala, edif, cap));
                System.out.println("Sala da turma alterada");
            } else {
                System.out.println("Esse número de turma não existe!");
            }
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }
    }

    /**
     *  Estado - Listar Turmas
     */
    private void listarTurmas() {
        try {
            System.out.println(this.model.getTurmas().toString());
        }
        catch (NullPointerException e) {
            System.out.println(e.getMessage());
        }
    }

    /**
     *  Estado - Adicionar Aluno a Turma
     */
    private void adicionarAlunoATurma() {
        try {
            System.out.println("Número da turma: ");
            String tid = scin.nextLine();
            if (this.model.existeTurma(tid)) {
                System.out.println("Número do aluno: ");
                String num = scin.nextLine();
                if (this.model.existeAluno(num)) {
                    this.model.adicionaAlunoTurma(tid, num);
                    System.out.println("Aluno adicionado à turma");
                } else {
                    System.out.println("Esse número de aluno não existe!");
                }
            } else {
                System.out.println("Esse número de turma não existe!");
            }
        }
        catch (NullPointerException e) {
            System.out.println(e.getMessage());
        }
    }

    /**
     *  Estado - Remover Aluno de Turma
     *
     *  Experimente implementar um menu com todos os alunos da turma. Os alunos deverão
     *  deixar de estar disponíveis no menu, conforme vão sendo removidos.
     *
     */
    private void removerAlunoDeTurma() {
        try {
            System.out.println("Número da turma: ");
            String tid = scin.nextLine();
            if (this.model.existeTurma(tid)) {
                // remover aluno(s)
            } else {
                System.out.println("Esse número de turma não existe!");
            }
        }
        catch (NullPointerException e) {
            System.out.println(e.getMessage());
        }
    }

    /**
     *  Estado - Listar Alunos da Turma
     */
    private void listarAlunosDaTurma() {
        try {
            System.out.println("Número da turma: ");
            String tid = scin.nextLine();
            System.out.println(this.model.getAlunos(tid).toString());
        }
        catch (NullPointerException e) {
            System.out.println(e.getMessage());
        }
    }
}
