using System.Windows;

namespace Anori.WPF.Conditions
{
    /// <summary>
    ///     Condition Collection Class
    /// </summary>
    /// <seealso cref="System.Windows.FreezableCollection{ConditionBase}" />
    public class ConditionCollection : FreezableCollection<ConditionBase>
    {
        /// <summary>
        ///     Determines whether the specified parameter is any.
        /// </summary>
        /// <param name="parameter">if set to <c>true</c> [parameter].</param>
        /// <returns>
        ///     <c>true</c> if the specified parameter is any; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAny(bool parameter = true)
        {
            bool any = false;
            foreach (ConditionBase condition in this)
            {
                if (condition.Inverted)
                {
                    if (condition.Evaluate() == parameter)
                    {
                        continue;
                    }
                } else
                {
                    if (condition.Evaluate() != parameter)
                    {
                        continue;
                    }
                }

                any = true;
                break;
            }

            return any;
        }

        /// <summary>
        ///     Is all of the conditions true or false.
        /// </summary>
        /// <param name="parameter">if set to <c>true</c> [parameter].</param>
        /// <returns>
        ///     <c>true</c> if the specified parameter is any; otherwise, <c>false</c>.
        /// </returns>
        public bool AreAll(bool parameter = true)
        {
            bool all = true;
            foreach (ConditionBase condition in this)
            {
                if (condition.Inverted)
                {
                    if (condition.Evaluate() != parameter)
                    {
                        continue;
                    }
                } else
                {
                    if (condition.Evaluate() == parameter)
                    {
                        continue;
                    }
                }

                all = false;
                break;
            }

            return all;
        }
    }
}
