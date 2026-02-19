import { source } from '@angular-devkit/schematics';
import nx from '@nx/eslint-plugin';
import { only } from 'node:test';

export default [
  ...nx.configs['flat/base'],
  ...nx.configs['flat/typescript'],
  ...nx.configs['flat/javascript'],
  {
    ignores: [
      '**/dist',
      '**/vite.config.*.timestamp*',
      '**/vitest.config.*.timestamp*',
    ],
  },
  {
    files: ['**/*.ts', '**/*.tsx', '**/*.js', '**/*.jsx'],
    rules: {
      '@nx/enforce-module-boundaries': [
        'error',
        {
          enforceBuildableLibDependency: true,
          allow: ['^.*/eslint(\\.base)?\\.config\\.[cm]?[jt]s$'],
          depConstraints: [
            { sourceTag: 'scope:shared', onlyDependOnLibsWithTags: ['scope:shared'] },
            { sourceTag: 'scope:shop', onlyDependOnLibsWithTags: ['scope:shop', 'scope:shared'] },
            { sourceTag: 'scope:api', onlyDependOnLibsWithTags: ['scope:api', 'scope:shared'] },

            { sourceTag: 'scope:security', onlyDependOnLibsWithTags: ['scope:security', 'scope:shared'] },
            { sourceTag: 'scope:staff-portal', onlyDependOnLibsWithTags: ['scope:security', 'scope:shared'] },
            { sourceTag: 'scope:passenger-portal', onlyDependOnLibsWithTags: ['scope:security', 'scope:shared']},
            { sourceTag: "type:app", onlyDependOnLibsWithTags: ['type:lib', 'type:data', 'type:ui', 'scope:security', 'scope:shared'] },

            { sourceTag: 'type:lib', onlyDependOnLibsWithTags: ['type:lib', 'type:data'] },
            { sourceTag: 'type:data', onlyDependOnLibsWithTags: ['type:data'] },

            { sourceTag: 'npm:public', onlyDependOnLibsWithTags: ['npm:public', 'scope:shared', 'scope:security', 'scope:api', 'scope:shop'] }
          ],
        },
      ],
    },
  },
  {
    files: [
      '**/*.ts',
      '**/*.tsx',
      '**/*.cts',
      '**/*.mts',
      '**/*.js',
      '**/*.jsx',
      '**/*.cjs',
      '**/*.mjs',
    ],
    // Override or add rules here
    rules: {},
  },
];
