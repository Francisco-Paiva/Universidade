/*
 *  DISCLAIMER: Este código foi criado para discussão e edição durante as aulas práticas de DSS, representando
 *  uma solução em construção. Como tal, não deverá ser visto como uma solução canónica, ou mesmo acabada.
 *  É disponibilizado para auxiliar o processo de estudo. Os alunos são encorajados a testar adequadamente o
 *  código fornecido e a procurar soluções alternativas, à medida que forem adquirindo mais conhecimentos.
 */
package uminho.dss.turmas3l.data;

import uminho.dss.turmas3l.business.Aluno;

import java.sql.*;
import java.util.*;

/**
 * Versão incompleta de um DAO para Alunos
 *
 * Falta implementar persisTência.
 *
 * @author JFC
 * @version 20211002
 */
public class AlunoDAO {

    // Queremos controlar nós a criação dos objectos
    private AlunoDAO() {}

    /**
     * Obter uma instância do Map de alunos. Escolhemos usar TreeMap
     *
     * @return uma instância vazia do Map de alunos
     */
    public static Map<String, Aluno> getInstance() {
        return new TreeMap<>();
    }
}
