/*
 *  DISCLAIMER: Este código foi criado para discussão e edição durante as aulas práticas de DSS, representando
 *  uma solução em construção. Como tal, não deverá ser visto como uma solução canónica, ou mesmo acabada.
 *  É disponibilizado para auxiliar o processo de estudo. Os alunos são encorajados a testar adequadamente o
 *  código fornecido e a procurar soluções alternativas, à medida que forem adquirindo mais conhecimentos.
 */
package uminho.dss.turmas3l.data;

import uminho.dss.turmas3l.business.Turma;

import java.util.HashMap;
import java.util.Map;

/**
 * Versão dummy de um DAO para Turmas
 *
 * Falta implementar persisTência.
 *
 * @author JFC
 * @version 20211002
 */
public class TurmaDAO  {

    // Queremos controlar nós a criação dos objectos
    private TurmaDAO() {}

    /**
     * Obter uma instância do Map de turmas. Escolhemos usar HashMap
     *
     * @return uma instância vazia do Map de turmas
     */
    public static Map<String, Turma> getInstance() {
         return new HashMap<>();
    }

}
