[![Build status](https://ci.appveyor.com/api/projects/status/tcxdale78y8vg07r/branch/master?svg=true)](https://ci.appveyor.com/project/twcclegg/RollingWindow/branch/master)
[![codecov](https://codecov.io/gh/twcclegg/RollingWindow/branch/master/graph/badge.svg)](https://codecov.io/gh/twcclegg/RollingWindow)

# Overview

This library provides a means to track the rolling maximum and minium of a stream of numbers (or anything IComparable) in O(1) time.

This has been benchmarked to be faster than the ascending minima algorithm.



Available on NuGet as package [`RollingWindow`](https://www.nuget.org/packages/RollingWindow).
