using System;

namespace dsl_play.language.Conditions;

public class ConditionsNotMetException() : 
    Exception("The conditions for applying these actions have not been met.");