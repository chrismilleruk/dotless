﻿using dotless.Exceptions;
using dotless.Infrastructure;
using dotless.Utils;

namespace dotless.Tree
{
  public class Variable : Node, IEvaluatable
  {
    public string Name { get; set; }

    public Variable(string name)
    {
      Name = name;
    }

    public override string ToCSS(Env env)
    {
      return Evaluate(env).ToCSS(env);
    }

    public override Node Evaluate(Env env)
    {
      var variable = env.Frames
        .SelectFirst(frame => frame.Variables()
                                .SelectFirst(v => v.Name == Name,
                                             v => v.Value is IEvaluatable ? v.Value.Evaluate(env) : v.Value));

      if (variable)
        return variable;

      throw new ParsingException("variable " + Name + " is undefined");
    }

  }
}