package rsi;
import java.lang.reflect.Method;


import java.util.Arrays;
import java.util.List;
import java.util.Vector;

public class Server {
    public Vector<String> show() {
        var methodList = new Vector<String>();
        methodList.addElement("intCalculate;Integer;Integer;String;Kalkulator");
        methodList.addElement("myHello;String;String;Powitanie");
        methodList.addElement("maxPrime;Integer;Integer;Liczba pierwsza");
        return methodList;
    }

    public Object callMethod(String methodName, Object[] params) {
        try {
            Class[] paramTypes = (Class[]) Arrays
               .stream(params)
               .map(Object::getClass)
               .toArray();

            var method = this.getClass()
                             .getMethod(methodName, paramTypes);
            return method.invoke(this, params);
        } catch (Exception e) {
        }
        return "Expection";
    }

    public String intCalculate(int a, int b, String c) {
        var builder = new StringBuilder();
        for (var operator: c.toCharArray()) {
            var resultStr = "";
            if (operator == '/' && b == 0) {
                resultStr = " operacja niedozwolona (dzielenie przez 0)";
            } else {
                var result = makeOperation(a, b, operator);
                resultStr = String.valueOf(result);
            }
            var equation = String.valueOf(a) + operator + String.valueOf(b) + "=" + resultStr;
            builder.append(equation + "\n");
        }
        return builder.toString();
    }

    private int makeOperation(int a, int b, char operator) {
        switch (operator) {
            case '+': return a + b;
            case '-': return a - b;
            case '*': return a * b;
            case '/': return a / b;
            case '%': return a % b;
            default: return 0;
        }
    }

    public String myHello(String name, String locate) {
        return "bla";
    }

    public int maxPrime(int delay, int value) {
        try {
            Thread.sleep(delay);
        } catch (Exception e) {

        }
        return 7 + value;
    }
}
