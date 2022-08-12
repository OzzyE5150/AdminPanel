import { global} from "@/store/global-store"

declare module '@vue/runtime-core' {
  interface ComponentCustomProperties {
    $store: global;
  }
}