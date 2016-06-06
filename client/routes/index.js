/**
 * F# Starter Kit (https://github.com/kriasoft/fsharp-starter-kit)
 *
 * Copyright © 2014-2016 Kriasoft, LLC. All rights reserved.
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE.txt file in the root directory of this source tree.
 */

import homeRoute from './home/route';
import aboutRoute from './about/route';
import errorRoute from './error/route';

const routes = {

  path: '/',

  children: [
    homeRoute,
    aboutRoute,
    errorRoute,
  ]

};

export default routes;
