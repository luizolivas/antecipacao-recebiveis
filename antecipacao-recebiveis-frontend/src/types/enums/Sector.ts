export const Sector = {
  0: "PRODUÇÃO",
  1: "SERVIÇO"
} as const;

export type Sector = keyof typeof Sector;
